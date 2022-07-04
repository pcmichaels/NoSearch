using NoSearch.Data.Resources;
using NoSearch.Models;

namespace NoSearch.App.Search
{
    public class SearchService : ISearchService
    {
        private readonly IResourceDataAccess _resourceDataAccess;

        public SearchService(IResourceDataAccess resourceDataAccess)
        {
            _resourceDataAccess = resourceDataAccess;
        }

        public IEnumerable<ResourceModel> GetRecent(int count)
        {
            var resources = _resourceDataAccess.GetLatest(count);
            return resources;
        }

        public IEnumerable<NoSearch.Models.ResourceModel> SearchResources(string searchText, bool isCasesSensitive)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return Enumerable.Empty<NoSearch.Models.ResourceModel>();
            }

            var resources = _resourceDataAccess
                .GetAllResources()
                .Where(a => (a.Name?.Contains(searchText, isCasesSensitive ?
                        StringComparison.InvariantCulture :
                        StringComparison.InvariantCultureIgnoreCase) ?? false) ||
                    (a.Description?.Contains(searchText, isCasesSensitive ?
                        StringComparison.InvariantCulture :
                        StringComparison.InvariantCultureIgnoreCase) ?? false));

            return resources;
        }
    }
}
