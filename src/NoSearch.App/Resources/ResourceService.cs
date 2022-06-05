﻿using NoSearch.Common;
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

            _resourceDataAccess.AddResource(resource);

            return Result<NoSearch.Models.Resource>.Success(resource);
        }

        public async Task<Result<Resource>> FindResource(Resource resource)
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

            resource.Name = resource.Name ?? data.Metadata.Title;
            resource.Description = resource.Description ?? data.Metadata.Description;
            resource.Uri = data.Metadata.Url;

            return Result<NoSearch.Models.Resource>.Success(resource);
        }

        public Task<Result<IEnumerable<Tag>>> GetAllTags()
        {
            var tags = _resourceDataAccess.GetAllTags();
            var result = Result<IEnumerable<Tag>>.Success(tags);
            return Task.FromResult(result);
        }
    }
}