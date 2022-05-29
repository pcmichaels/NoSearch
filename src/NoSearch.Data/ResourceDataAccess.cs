using NoSearch.Models;

namespace NoSearch.Data
{
    public class ResourceDataAccess : IResourceDataAccess
    {
        public IEnumerable<Resource> GetAllResources()
        {
            return new List<Resource>()
            {
                new Resource("Excalidraw", "Online diagramming", "https://excalidraw.com/"),
                new Resource("Hacker News", "Tech News", "https://news.ycombinator.com/"),
                new Resource("Stack Overflow", "Online Q&A site for programmers", "https://stackoverflow.com/")
            };
        }
    }
}