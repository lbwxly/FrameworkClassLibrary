using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FCL.Extentsion.Reflection
{
    public static class AssemblyExtension
    {
        /// <summary>
        /// Gets the the directory path in which current executing assembly(this extension assembly) is placed.(including splash \)
        /// </summary>
        /// <returns></returns>
        public static string GetWorkingDirectory()
        {
            return Assembly.GetExecutingAssembly().GetAssemblyDirectory();
        }

        public static string GetAssemblyDirectory(this Assembly assembly)
        {
            return Path.GetDirectoryName(new Uri(assembly.CodeBase).AbsolutePath) + "\\";
        }
    }
}
