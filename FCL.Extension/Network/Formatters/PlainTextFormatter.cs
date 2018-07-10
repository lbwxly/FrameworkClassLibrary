using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCL.Network.Formatters
{
    public class PlainTextFormatter : Formatter
    {
        public override T Deserialize<T>(string text)
        {
            if (typeof(T) != typeof(string))
            {
                throw new NotSupportedException("don't support target type which is not string");
            }

            return text as T;
        }

        public override string Serialize<T>(T obj)
        {
            if (obj == null)
            {
                return null;
            }

            return obj.ToString();
        }
    }
}
