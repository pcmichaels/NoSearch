using NoSearch.Models;

namespace NoSearch.App.Models
{
    public class MainViewModel
    {
        public int TotalNumberOfResources { get; set; }
        public SearchViewModel SearchViewModel { get; set; } = new SearchViewModel();
        public IReadOnlyList<ResourceModel> Recent { get; set; } = new List<ResourceModel>();
    }
}
