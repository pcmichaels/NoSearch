using NoSearch.Models;

namespace NoSearch.Data.Resources
{
    public interface IResourceDataAccess
    {
        IEnumerable<ResourceModel> GetAllResources();
        IEnumerable<TagModel> GetAllTags();
        Task AddResource(ResourceModel resource);
        IEnumerable<ResourceModel> GetLatest(int count);
    }
}
