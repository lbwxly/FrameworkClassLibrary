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
            return (long)(datetime - UTCStartingDate).TotalMilliseconds;
        }

        public static long ToUnixTimestampFromLocalTime(this DateTime datetime)
        {
            return (long)(datetime - LocalStartingDate).TotalMilliseconds;
        }

        public static DateTime EndOfTheDay(this DateTime datetime)
        {
            return new DateTime(datetime.Year, datetime.Month, datetime.Day, 23, 59, 59);
        }

        public static DateTime EndOfToday()
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
        }

        public static DateTime StartOfTheDay(this DateTime datetime)
        {
            return new DateTime(datetime.Year, datetime.Month, datetime.Day, 0, 0, 0);
        }

        public static DateTime StartOfToday()
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
        }

        public static DateTime ToDateTime(this long timeStamp)
        {
            return UTCStartingDate.AddMilliseconds(timeStamp);
        }

        public static DateTime ToLocalDateTime(this long timeStamp)
        {
            return LocalStartingDate.AddMilliseconds(timeStamp);
        }

        private static DateTime UTCStartingDate
        {
            get
            {
                return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            }
        }

        public static DateTime LocalStartingDate
        {
            get
            {
                return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);
            }
        }
    }
}
