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

        public void AddResource(ResourceModel resourceModel)
        {
            var resourceEntity = new Resource()
            {
                Name = resourceModel.Name,
                Description = resourceModel.Description,
                Rank = resourceModel.Rank,
                Uri = resourceModel.Uri
            };
            _noSearchDbContext.Add(resourceEntity);
            _noSearchDbContext.SaveChanges();
        }

        public IEnumerable<ResourceModel> GetAllResources()
        {
            var resources = _noSearchDbContext
                .Resources
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
                .OrderByDescending(a => a.DateAdded)
                .Take(count);

            return resources;
        }
    }
}