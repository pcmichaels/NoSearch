using NoSearch.Common;
using NoSearch.Models;

namespace NoSearch.App.Resources
{
    public interface IResourceService
    {
        Task<DataResult<Resource>> AddResource(Resource resource);
        Task<DataResult<Resource>> FindResource(Resource newResource);
        Task<DataResult<IEnumerable<Tag>>> GetAllTags();
    }
}
