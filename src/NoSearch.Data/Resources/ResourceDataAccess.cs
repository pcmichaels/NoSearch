using NoSearch.Models;
using System.Reflection;
using System.Text.Json;

namespace NoSearch.Data.Resources
{
    public class ResourceDataAccess : IResourceDataAccess
    {
        public void AddResource(ResourceModel resource)
        {
            throw new NotImplementedException();
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
            string executingDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
            string jsonData = File.ReadAllText($"{executingDir}/Data/TagList.json");
            var tags = JsonSerializer.Deserialize<IEnumerable<TagModel>>(jsonData);

            if (tags == null)
            {
                throw new Exception("No data file found");
            }

            return tags;
        }

    }
}