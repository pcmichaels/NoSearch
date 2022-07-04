namespace NoSearch.App.Models
{
    public class SubmitNewViewModel
    {
        public NoSearch.Models.ResourceModel NewResource { get; set; } = new NoSearch.Models.ResourceModel();
        public bool IsValidated { get; set; }
        public string? Error { get; set; }
        public IEnumerable<string>? AllTags { get; set; }
    }
}
