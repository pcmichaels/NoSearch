using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoSearch.App.Models;
using NoSearch.App.Resources;
using NoSearch.App.Search;
using NoSearch.Data;
using System.Diagnostics;

namespace NoSearch.App.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISearchService _searchService;
        private readonly IResourceService _resourceService;

        public HomeController(ILogger<HomeController> logger,
            ISearchService searchService, 
            IResourceService resourceService)
        {
            _logger = logger;
            _searchService = searchService;
            _resourceService = resourceService;
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
        
        public IActionResult SubmitNew()
        {
            return View(new SubmitNewViewModel());            
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
            }

            return View("SubmitNew", submitNewViewModel);
        }

        [HttpPost]        
        public async Task<IActionResult> SubmitNew(SubmitNewViewModel submitNewViewModel)
        {
            var result = await _resourceService.AddResource(submitNewViewModel.NewResource);
            if (!result.IsSuccess)
            {
                submitNewViewModel.Error = result.Errors.First();
            }

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