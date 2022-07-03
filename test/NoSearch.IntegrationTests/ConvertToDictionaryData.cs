using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NoSearch.IntegrationTests
{
    public class ConvertToDictionaryData
    {
        public static Dictionary<string, string> ConvertToFormContent<T>(T toConvert)
        {            
            var kvpList = ReadObject(
                typeof(T).Assembly, 
                typeof(T), 
                toConvert,
                null);

            return new Dictionary<string, string>(kvpList);
        }

        private static List<KeyValuePair<string, string>> ReadObject(
            Assembly assembly, Type objectType, object toConvert, string? parentObjectName)
        {
            var kvpList = new List<KeyValuePair<string, string>>();
            var props = objectType.GetProperties();

            foreach (var prop in props)
            {
                var value = prop.GetValue(toConvert);
                if (value == null) continue;

                if (prop.GetType().IsClass && !(prop.PropertyType.Assembly.FullName?.Contains("System") ?? false))
                {
                    Type propType = prop.PropertyType.Assembly.GetType(prop.PropertyType.FullName);

                    if (propType != null)
                    {
                        var result = ReadObject(
                            assembly,
                            propType,
                            value,
                            prop.Name);
                    }
                }
                
                kvpList.Add(new KeyValuePair<string, string>(
                    formatParentObjectName(parentObjectName, prop.Name), value!.ToString()));                
            }

            return kvpList;

            string formatParentObjectName(string? parentName, string propName) =>
                string.IsNullOrWhiteSpace(parentObjectName)
                ? propName : $"{parentObjectName}.prop.Name";
        }
    }
}
