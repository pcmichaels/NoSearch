namespace NoSearch.Models
{
    public class ResourceModel
    {
        public ResourceModel(string name, string description,
            string uri)
        {
            Name = name;
            Description = description;
            Uri = uri;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Uri { get; set; }
        public IEnumerable<string>? Tags { get; set; }
    }
}
