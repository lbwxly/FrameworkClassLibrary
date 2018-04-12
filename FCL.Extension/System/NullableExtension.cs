using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCL.System
{
    public static class NullableExtension
    {
        public static string ToString(this decimal? decimalValue, string format)
        {
            if (decimalValue == null)
            {
                return string.Empty;
            }

            return decimalValue.Value.ToString(format);
        }
    }
}
