using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoSearch.App.Models;
using NoSearch.App.Search;
using NoSearch.Data;
using System.Diagnostics;

namespace NoSearch.App.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISearchService _searchService;

        public HomeController(ILogger<HomeController> logger,
            ISearchService searchService)
        {
            _logger = logger;
            _searchService = searchService;
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
            return View();
        }

        [HttpPost]
        public IActionResult SubmitNew(SubmitNewViewModel submitNewViewModel)
        {
            _resourceService.AddResource();

            return View(submitNewViewModel);
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}