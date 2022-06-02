using NoSearch.Common;

namespace NoSearch.App.Resources
{
    public interface IResourceService
    {
        Task<Result<NoSearch.Models.Resource>> AddResource(NoSearch.Models.Resource resource);
    }
}
