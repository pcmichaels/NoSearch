using NoSearch.Models;

namespace NoSearch.Data.Resources
{
    public class Resource : ResourceModel
    {
        public int Id { get; set; }

        public override Tag[] Tags { get; }
    }
}
