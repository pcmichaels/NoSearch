namespace NoSearch.Models
{
    public class TagModel
    {
        public TagModel(string name)
        {
            Name = name;
            IsRestricted = false;
        }

        public string Name { get; set; }
        public bool IsRestricted { get; set; } = false;
    }
}
