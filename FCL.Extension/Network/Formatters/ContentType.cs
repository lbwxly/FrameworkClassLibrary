using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCL.Network.Formatters
{
    public enum ContentType
    {
        Json,
        Plain,
        FormUrlEncode
    }

    internal static class WebUtilExtension
    {
        public static string ToMimeTypeString(this ContentType contentType)
        {
            switch (contentType)
            {
                case ContentType.Json:
                    return "application/json";
                case ContentType.FormUrlEncode:
                    return "application/x-www-form-urlencoded";
                case ContentType.Plain:
                    return "text/plain";
            }

            return null;
        }
    }
}
