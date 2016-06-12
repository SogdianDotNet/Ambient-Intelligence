using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SD.Commons.Shared.Validation
{
    /// <summary>
    /// class Validation.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public static class Validation<TEntity>
    {
        /// <summary>
        /// Gets all the string properties from the entity object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<string> GetStringProperties(TEntity obj)
        {
            List<string> returnValue = new List<string>();
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                if (prop.PropertyType == typeof(string) && prop.PropertyType != typeof(Nullable<>))
                {
                    returnValue.Add(prop.GetValue(obj).ToString());
                }
            }
            return returnValue;
        }

        /// <summary>
        /// Validation method for the entity.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="strings"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static bool Validate(TEntity obj, List<string> strings, ref List<ErrorMessage> errors)
        {
            if (obj != null && ValidateStrings(strings) && !errors.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Validates all the string properties of the list. Checking foreach string if it's null or empty.
        /// </summary>
        /// <param name="strings"></param>
        /// <returns></returns>
        private static bool ValidateStrings(List<string> strings)
        {
            List<string> ret = new List<string>();
            if (strings == null)
            {
                return true;
            }
            else if (strings != null)
            {
                foreach (var str in strings)
                {
                    if (string.IsNullOrEmpty(str))
                    {
                        ret.Add(str);
                    }
                }
                if (ret.Any())
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
