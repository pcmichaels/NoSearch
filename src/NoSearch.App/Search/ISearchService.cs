using NoSearch.Models;

namespace NoSearch.App.Search
{
    public interface ISearchService
    {
        IEnumerable<ResourceModel> SearchResources(string searchText, bool isCasesSensitive);
        IEnumerable<ResourceModel> GetRecent(int count);
    }
}