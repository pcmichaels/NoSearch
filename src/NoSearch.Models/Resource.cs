namespace NoSearch.Models
{
    public class Resource
    {
        public Resource(string name, 
            string description, 
            string uri)
        {
            Name = name;
            Description = description;            
            Uri = uri;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int? Rank { get; set; }
        public string Uri { get; set; }
    }
}