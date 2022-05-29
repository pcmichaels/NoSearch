using NoSearch.Models;

namespace NoSearch.App.Models
{
    public class SearchViewModel
    {
        public string SearchTerm { get; set; }
        public IReadOnlyList<Resource> Resources { get; set; }
    }
}
