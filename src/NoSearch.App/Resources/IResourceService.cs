using NoSearch.Common;
using NoSearch.Models;

namespace NoSearch.App.Resources
{
    public interface IResourceService
    {
        Task<Result<Resource>> AddResource(Resource resource);
        Task<Result<Resource>> FindResource(Resource newResource);
        Task<Result<IEnumerable<Tag>>> GetAllTags();
    }
}
