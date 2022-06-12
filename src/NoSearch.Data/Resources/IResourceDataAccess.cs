using NoSearch.Models;

namespace NoSearch.Data.Resources
{
    public interface IResourceDataAccess
    {
        IEnumerable<TagModel> GetAllTags();
        IEnumerable<ResourceModel> GetAllResources();
        Task AddResource(ResourceModel resource);
    }
}
