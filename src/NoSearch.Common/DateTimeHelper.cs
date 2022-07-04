using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSearch.Common
{
    public class DateTimeHelper : IDateTimeHelper
    {
        public DateTimeOffset GetCurrentTime()
        {
            return DateTime.Now;
        }
    }
}
