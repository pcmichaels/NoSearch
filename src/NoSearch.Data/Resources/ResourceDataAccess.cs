using NoSearch.Data.DataAccess;
using NoSearch.Models;
using System.Reflection;
using System.Text.Json;

namespace NoSearch.Data.Resources
{
    public class ResourceDataAccess : IResourceDataAccess
    {
        private readonly NoSearchDbContext _noSearchDbContext;

        public ResourceDataAccess(NoSearchDbContext noSearchDbContext)
        {
            _noSearchDbContext = noSearchDbContext;
        }

        public async Task AddResource(ResourceModel resourceModel)
        {
            var resourceEntity = new Resource()
            {
                Name = resourceModel.Name,
                Description = resourceModel.Description,
                Rank = resourceModel.Rank,
                Uri = resourceModel.Uri,
                DateAdded = resourceModel.DateAdded
            };
            _noSearchDbContext.Add(resourceEntity);
            await _noSearchDbContext.SaveChangesAsync();
        }

        public IEnumerable<ResourceModel> GetAllResources()
        {
            var resources = _noSearchDbContext
                .Resources
                .Where(a => a.IsApproved)
                .Select(a => new ResourceModel()
                {
                    Description = a.Description,
                    Name = a.Name,
                    Rank = a.Rank,
                    Uri = a.Uri
                });
            return resources;
        }

        public IEnumerable<TagModel> GetAllTags()
        {
            var tags = _noSearchDbContext.Tags;
            return tags;
        }

        public IEnumerable<ResourceModel> GetLatest(int count)
        {
            var resources = _noSearchDbContext
                .Resources
                .Where(a => a.IsApproved)
                .OrderByDescending(a => a.DateAdded)
                .Take(count);

            return resources;
        }

        public ResourceModel? GetResurceByUrl(string url)
        {
            var resources = _noSearchDbContext
                .Resources
                .Where(a => a.IsApproved 
                    && a.Uri == url);

            if (resources == null || !resources.Any())
                return null;

            if (resources.Count() > 1)
            {
                // Log error - there's more than
                // one resource with the same URL
            }

            return resources.First();
        }
    }
}