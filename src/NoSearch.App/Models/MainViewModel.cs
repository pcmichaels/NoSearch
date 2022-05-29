namespace NoSearch.App.Models
{
    public class MainViewModel
    {
        public int TotalNumberOfResources { get; set; }
        public SearchViewModel SearchViewModel { get; set; } = new SearchViewModel();
    }
}
