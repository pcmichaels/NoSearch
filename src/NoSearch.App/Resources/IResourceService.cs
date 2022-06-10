using NoSearch.Common;
using NoSearch.Models;

namespace NoSearch.App.Resources
{
    public interface IResourceService
    {
        Task<DataResult<ResourceModel>> AddResource(ResourceModel resource);
        Task<DataResult<ResourceModel>> FindResource(ResourceModel newResource);
        Task<DataResult<IEnumerable<TagModel>>> GetAllTags();
    }
}
