using NoSearch.Data;
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

        public IEnumerable<Resource> SearchResources(string searchText, bool isCasesSensitive)
        {
            var resources = _resourceDataAccess
                .GetAllResources()
                .Where(a => a.Name.Contains(searchText, isCasesSensitive ? 
                        StringComparison.InvariantCulture :        
                        StringComparison.InvariantCultureIgnoreCase) ||
                    a.Description.Contains(searchText, isCasesSensitive ?
                        StringComparison.InvariantCulture :
                        StringComparison.InvariantCultureIgnoreCase));

            return resources;
        }
    }
}
