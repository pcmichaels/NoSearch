using NoSearch.Models;
using System.ComponentModel;

namespace NoSearch.App.Models
{
    public class SearchViewModel
    {
        [DisplayName("Search")]
        public string SearchTerm { get; set; }
        public IReadOnlyList<ResourceModel> Resources { get; set; }
    }
}
