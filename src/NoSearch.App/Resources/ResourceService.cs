using NoSearch.Common;
using NoSearch.Data.Resources;
using NoSearch.Models;
using WebSiteMeta.Scraper;

namespace NoSearch.App.Resources
{
    public class ResourceService : IResourceService
    {
        private readonly IResourceDataAccess _resourceDataAccess;
        private readonly IFindMetaData _findMetaData;

        public ResourceService(IResourceDataAccess resourceDataAccess,
            IFindMetaData findMetaData)
        {
            _resourceDataAccess = resourceDataAccess;
            _findMetaData = findMetaData;
        }

        public async Task<DataResult<NoSearch.Models.ResourceModel>> AddResource(NoSearch.Models.ResourceModel resource)
        {
            string cleanUrl = _findMetaData.CleanUrl(resource.Uri);
            bool isValid = _findMetaData.ValidateUrl(cleanUrl);
            if (!isValid)
            {
                return DataResult<ResourceModel>.Fail($"Invalid URL provided: {resource.Uri}, cleaned to {cleanUrl}");
            }

            _resourceDataAccess.AddResource(resource);

            return DataResult<NoSearch.Models.ResourceModel>.Success(resource);
        }

        public async Task<DataResult<ResourceModel>> FindResource(ResourceModel resource)
        {
            string cleanUrl = _findMetaData.CleanUrl(resource.Uri);
            bool isValid = _findMetaData.ValidateUrl(cleanUrl);
            if (!isValid)
            {
                return DataResult<ResourceModel>.Fail($"Invalid URL provided: {resource.Uri}, cleaned to {cleanUrl}");
            }

            var data = await _findMetaData.Run(cleanUrl);
            if (!data.IsSuccess)
            {
                return DataResult<ResourceModel>.Fail($"Invalid URL provided: {resource.Uri}");
            }

            resource.Name = resource.Name ?? data.Metadata.Title;
            resource.Description = resource.Description ?? data.Metadata.Description;
            resource.Uri = data.Metadata.Url;

            return DataResult<NoSearch.Models.ResourceModel>.Success(resource);
        }

        public Task<DataResult<IEnumerable<TagModel>>> GetAllTags()
        {
            var tags = _resourceDataAccess.GetAllTags();
            var result = DataResult<IEnumerable<TagModel>>.Success(tags);
            return Task.FromResult(result);
        }
    }
}
