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
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly ILogger<ResourceService> _logger;

        public ResourceService(IResourceDataAccess resourceDataAccess,
            IFindMetaData findMetaData,
            IDateTimeHelper dateTimeHelper,
            ILogger<ResourceService> logger)
        {
            _resourceDataAccess = resourceDataAccess;
            _findMetaData = findMetaData;
            _dateTimeHelper = dateTimeHelper;
            _logger = logger;
        }

        public async Task<DataResult<NoSearch.Models.ResourceModel>> AddResource(NoSearch.Models.ResourceModel resource)
        {
            string cleanUrl = _findMetaData.CleanUrl(resource.Uri);
            bool isValid = _findMetaData.ValidateUrl(cleanUrl);
            if (!isValid)
            {
                return DataResult<ResourceModel>.Fail(
                    $"Invalid URL provided: {resource.Uri}, cleaned to {cleanUrl}");
            }

            var existingResource = _resourceDataAccess.GetResurceByUrl(cleanUrl);
            if (existingResource != null)
            {
                return DataResult<ResourceModel>.Fail(
                    $"URL has already been added");
            }

            resource.DateAdded = _dateTimeHelper.GetCurrentDate();
            await _resourceDataAccess.AddResource(resource);

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
                // return DataResult<ResourceModel>.Fail($"Unable to find metadata: {resource.Uri}");
                _logger.LogWarning($"Unable to find metadata: {resource.Uri}");
            }

            resource.Name = resource.Name ?? data?.Metadata?.Title ?? String.Empty;
            resource.Description = resource.Description ?? data?.Metadata?.Description ?? String.Empty;
            resource.Uri = data?.Metadata?.Url ?? String.Empty;

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
