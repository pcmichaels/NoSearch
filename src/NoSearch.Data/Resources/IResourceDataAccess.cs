using NoSearch.Models;

namespace NoSearch.Data.Resources
{
    public interface IResourceDataAccess
    {
        IEnumerable<ResourceModel> GetAllResources();
        IEnumerable<TagModel> GetAllTags();
        void AddResource(ResourceModel resource);
    }
}
