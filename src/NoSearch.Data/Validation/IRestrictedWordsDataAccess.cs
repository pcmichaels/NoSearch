namespace NoSearch.Data.Validation
{
    public interface IRestrictedWordsDataAccess
    {
        IEnumerable<string>? GetAll();
    }
}
