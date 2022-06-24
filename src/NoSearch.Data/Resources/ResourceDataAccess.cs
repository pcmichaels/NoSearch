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
        }

        public IEnumerable<ResourceModel> GetAllResources()
        {
            string executingDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
            string jsonData = File.ReadAllText($"{executingDir}/Data/ResourceList.json");
            var resources = JsonSerializer.Deserialize<IEnumerable<ResourceModel>>(jsonData);

            if (resources == null)
            {
                throw new Exception("No data file found");
            }

            return resources;
        }

        public IEnumerable<TagModel> GetAllTags()
        {
            var tags = _noSearchDbContext.Tags;
            return tags;
        }

    }
}