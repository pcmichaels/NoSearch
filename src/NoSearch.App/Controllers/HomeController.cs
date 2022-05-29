using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoSearch.App.Models;
using NoSearch.Data;
using System.Diagnostics;

namespace NoSearch.App.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IResourceDataAccess _resourceDataAccess;

        public HomeController(ILogger<HomeController> logger,
            IResourceDataAccess resourceDataAccess)
        {
            _logger = logger;
            _resourceDataAccess = resourceDataAccess;
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

            var resources = _resourceDataAccess
                .GetAllResources()
                .Where(a => a.Name.Contains(searchText) ||
                    a.Description.Contains(searchText));

            var searchViewModel = new SearchViewModel()
            {
                SearchTerm = searchText,
                Resources = resources.ToList()
            };

            mainViewModel.SearchViewModel = searchViewModel;
            return View(mainViewModel);
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}