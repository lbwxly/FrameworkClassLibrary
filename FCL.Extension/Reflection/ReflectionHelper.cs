using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCL.Reflection
{
    public class ReflectionHelper
    {
        public static T GetValueByPath<T>(object obj, string path) where T : class
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }
            object targetObject = obj;
            string[] pathComponents = path.Split('.');
            foreach (var component in pathComponents)
            {
                targetObject = targetObject.GetType().GetProperty(component).GetValue(targetObject);
                if (targetObject == null)
                {
                    return null;
                }
            }

            return targetObject as T;
        }
    }
}
