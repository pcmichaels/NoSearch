using NoSearch.Common;
using NoSearch.Data;
using NoSearch.Models;

namespace NoSearch.App.Resources
{
    public class ValidationService : IValidationService
    {
        private readonly IRestrictedWordsDataAccess _restrictedWordsDataAccess;
        private IEnumerable<string> _blockedWords;
       
        public ValidationService(IRestrictedWordsDataAccess restrictedWordsDataAccess)
        {            
            _restrictedWordsDataAccess = restrictedWordsDataAccess;
        }

        public Result ValidateResource(Resource resource)
        {
            if (_blockedWords == null)
            {
                _blockedWords = _restrictedWordsDataAccess.GetAll();
            }

            if (_blockedWords.Any(a => resource.Name.Contains(a, StringComparison.OrdinalIgnoreCase))
                || _blockedWords.Any(a => resource.Description.Contains(a, StringComparison.OrdinalIgnoreCase)))
            {
                return Result.Fail("Unacceptable Name or Description");
            }
            return Result.Success();
        }
    }
}
