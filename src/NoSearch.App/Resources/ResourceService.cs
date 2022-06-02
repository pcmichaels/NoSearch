using NoSearch.Common;
using NoSearch.Data;
using NoSearch.Models;
using System.ComponentModel.Design;
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

        public async Task<Result<NoSearch.Models.Resource>> AddResource(NoSearch.Models.Resource resource)
        {
            string cleanUrl = _findMetaData.CleanUrl(resource.Uri);
            bool isValid = _findMetaData.ValidateUrl(cleanUrl);
            if (!isValid)
            {
                return Result<Resource>.Fail($"Invalid URL provided: {resource.Uri}, cleaned to {cleanUrl}");
            }

            var data = await _findMetaData.Run(cleanUrl);
            if (!data.IsSuccess)
            {
                return Result<Resource>.Fail($"Invalid URL provided: {resource.Uri}");
            }

            _resourceDataAccess.AddResource(resource);

            return Result<NoSearch.Models.Resource>.Success(resource);
        }
    }
}
