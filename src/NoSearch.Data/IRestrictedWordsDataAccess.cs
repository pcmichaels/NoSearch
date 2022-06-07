namespace NoSearch.Data
{
    public interface IRestrictedWordsDataAccess
    {
        IEnumerable<string> GetAll();
    }
}