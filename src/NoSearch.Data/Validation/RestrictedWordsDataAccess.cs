using NoSearch.Data.DataAccess;
using System.Reflection;
using System.Text.Json;

namespace NoSearch.Data.Validation
{
    public class RestrictedWordsDataAccess : IRestrictedWordsDataAccess
    {
        private readonly NoSearchDbContext _noSearchDbContext;

        public RestrictedWordsDataAccess(NoSearchDbContext noSearchDbContext)
        {
            _noSearchDbContext = noSearchDbContext;
        }

        public IEnumerable<string> GetAll()
        {
            var restrictedWords = _noSearchDbContext
                .RestrictedWords
                .Where(a => a.Reason == 1);

            return restrictedWords.Select(a => a.Word);
        }

    }
}
