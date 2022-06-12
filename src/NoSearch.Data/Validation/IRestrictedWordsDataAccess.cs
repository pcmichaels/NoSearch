using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSearch.Data.Validation
{
    public interface IRestrictedWordsDataAccess
    {
        IEnumerable<string>? GetAll();
    }
}
