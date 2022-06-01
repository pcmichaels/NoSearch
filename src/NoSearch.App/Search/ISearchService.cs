using NoSearch.Models;

namespace NoSearch.App.Search
{
    public interface ISearchService
    {
        IEnumerable<Resource> SearchResources(string searchText, bool isCasesSensitive);
    }
}