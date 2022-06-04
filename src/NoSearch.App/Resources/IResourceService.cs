using NoSearch.Common;
using NoSearch.Models;

namespace NoSearch.App.Resources
{
    public interface IResourceService
    {
        Task<Result<NoSearch.Models.Resource>> AddResource(Resource resource);
        Task<Result<NoSearch.Models.Resource>> FindResource(Resource newResource);
    }
}
