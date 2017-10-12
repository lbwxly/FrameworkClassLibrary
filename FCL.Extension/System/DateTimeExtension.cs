using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCL.System
{
    public static class DateTimeExtension
    {
        public static long ToUnixTimestamp(this DateTime datetime)
        {
            return (long)(datetime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }
    }
}
