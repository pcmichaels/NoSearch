using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoSearch.App.Models;
using NoSearch.App.Resources;
using NoSearch.App.Search;
using System.Diagnostics;

namespace NoSearch.App.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISearchService _searchService;
        private readonly IResourceService _resourceService;
        private readonly IValidationService _validationService;

        public HomeController(ILogger<HomeController> logger,
            ISearchService searchService,
            IResourceService resourceService,
            IValidationService validationService)
        {
            _logger = logger;
            _searchService = searchService;
            _resourceService = resourceService;
            _validationService = validationService;
        }

        public IActionResult Index()
        {
            var model = new MainViewModel()
            {
                SearchViewModel = new SearchViewModel(),
                TotalNumberOfResources = 100
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(MainViewModel mainViewModel)
        {
            string searchText = mainViewModel.SearchViewModel.SearchTerm;

            var resources = _searchService.SearchResources(searchText, false);

            var searchViewModel = new SearchViewModel()
            {
                SearchTerm = searchText,
                Resources = resources.ToList()
            };

            mainViewModel.SearchViewModel = searchViewModel;
            return View(mainViewModel);
        }

        public async Task<IActionResult> SubmitNew()
        {
            var tags = await _resourceService.GetAllTags();
            var viewModel = new SubmitNewViewModel()
            {
                AllTags = tags.Data.Select(a => a.Name)
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Lookup(SubmitNewViewModel submitNewViewModel)
        {
            var result = await _resourceService.FindResource(submitNewViewModel.NewResource);
            if (!result.IsSuccess)
            {
                submitNewViewModel.Error = result.Errors.First();
            }
            else
            {
                ModelState.Clear();
                submitNewViewModel.NewResource = result.Data;

                await UpdateTags(submitNewViewModel);
            }

            return View("SubmitNew", submitNewViewModel);
        }

        private async Task UpdateTags(SubmitNewViewModel submitNewViewModel)
        {
            if (!(submitNewViewModel?.AllTags?.Any() ?? false))
            {
                var tags = await _resourceService.GetAllTags();
                if (tags.IsSuccess)
                    submitNewViewModel.AllTags = tags.Data.Select(a => a.Name);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SubmitNew(SubmitNewViewModel submitNewViewModel)
        {
            ArgumentNullException.ThrowIfNull(submitNewViewModel.NewResource);

            var validationResult = _validationService.ValidateResource(submitNewViewModel.NewResource);
            if (!validationResult.IsSuccess)
            {
                submitNewViewModel.Error = validationResult.Errors.First();
            }
            else
            {
                var result = await _resourceService.AddResource(submitNewViewModel.NewResource);
                if (!result.IsSuccess)
                {
                    submitNewViewModel.Error = result.Errors.First();
                }
            }
            await UpdateTags(submitNewViewModel);

            return View("SubmitNew", submitNewViewModel);
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}