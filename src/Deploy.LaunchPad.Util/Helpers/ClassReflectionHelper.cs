using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Util.Helpers
{
    public partial class ClassReflectionHelper : HelperBase
    {

        /// <summary>
        /// Gets PropertyInfo of a nested property. Used to help with sorting deep properties.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyPath"></param>
        /// <returns></returns>
        public virtual PropertyInfo GetNestedPropertyInfo(Type type, string propertyPath)
        {
            string[] properties = propertyPath.Split('.');
            PropertyInfo propertyInfo = null;

            foreach (var property in properties)
            {
                propertyInfo = type.GetProperty(property, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo == null)
                {
                    return null;
                }
                type = propertyInfo.PropertyType;
            }

            return propertyInfo;
        }

        /// <summary>
        ///  Gets the value of a nested property.  Used to help with sorting deep properties
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyPath"></param>
        /// <returns></returns>
        public virtual object GetNestedPropertyValue(object obj, string propertyPath)
        {
            foreach (var property in propertyPath.Split('.'))
            {
                if (obj == null) return null;
                var prop = obj.GetType().GetProperty(property, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (prop == null) return null;
                obj = prop.GetValue(obj, null);
            }
            return obj;
        }
    }
}
