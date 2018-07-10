using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCL.Network.Formatters
{
    public abstract class Formatter
    {
        public abstract string Serialize<T>(T obj) where T : class;
        public abstract T Deserialize<T>(string text) where T : class;

        public static Formatter GetFormatter(ContentType type)
        {
            switch (type)
            {
                case ContentType.Json:
                    return new JsonFormatter();
                case ContentType.FormUrlEncode:
                case ContentType.Plain:
                    return new PlainTextFormatter();
            }

            return null;
        }
    }
}
