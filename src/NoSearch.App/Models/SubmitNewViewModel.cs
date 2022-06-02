using NoSearch.Models;

namespace NoSearch.App.Models
{
    public class SubmitNewViewModel
    {
        public NoSearch.Models.Resource NewResource { get; set; }
        public bool IsValidated { get; set; }
    }
}
