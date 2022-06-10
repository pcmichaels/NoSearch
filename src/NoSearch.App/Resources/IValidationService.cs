using NoSearch.Common;
using NoSearch.Models;

namespace NoSearch.App.Resources
{
    public interface IValidationService
    {
        Result ValidateResource(ResourceModel resource);
    }
}