using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCL.Misc
{
    public class AppSettingReader
    {
        public static int ReadInt(string key)
        {
            return int.Parse(ConfigurationManager.AppSettings[key]);
        }
    }
}
