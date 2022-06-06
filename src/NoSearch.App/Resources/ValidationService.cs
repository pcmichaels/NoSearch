using NoSearch.Common;
using NoSearch.Models;

namespace NoSearch.App.Resources
{
    public class ValidationService : IValidationService
    {
        public IEnumerable<string> BlockedWords { get; set; }

        public ValidationService(IEnumerable<string> blockedWords)
        {
            BlockedWords = blockedWords;
        }

        public Result ValidateResource(Resource resource)
        {
            if (BlockedWords.Any(a => resource.Name.Contains(a, StringComparison.OrdinalIgnoreCase))
                || BlockedWords.Any(a => resource.Description.Contains(a, StringComparison.OrdinalIgnoreCase)))
            {
                return Result.Fail("Unacceptable Name or Description");
            }
            return Result.Success();
        }
    }
}
