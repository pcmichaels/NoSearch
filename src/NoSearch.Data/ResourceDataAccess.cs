using NoSearch.Models;
using System.Reflection;
using System.Text.Json;

namespace NoSearch.Data
{
    public class ResourceDataAccess : IResourceDataAccess
    {
        public void AddResource(Resource resource)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Resource> GetAllResources()
        {
            string executingDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
            string jsonData = File.ReadAllText($"{executingDir}/Data/ResourceList.json");
            var resources = JsonSerializer.Deserialize<IEnumerable<Resource>>(jsonData);

            if (resources == null)
            {
                throw new Exception("No data file found");
            }

            return resources;
        }
    }
}