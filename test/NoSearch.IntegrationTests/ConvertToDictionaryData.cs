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
            ArgumentNullException.ThrowIfNull(toConvert);

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

                var propTypeAssembly = prop.PropertyType.Assembly;
                if (propTypeAssembly == null) throw new Exception(
                    $"Could not find assembly for {prop.PropertyType}");

                if (prop.GetType().IsClass && !(propTypeAssembly.FullName?.Contains("System") ?? false))
                {
                    string propertyTypeName = prop.PropertyType.FullName!;
                    Type? propType = propTypeAssembly.GetType(propertyTypeName);

                    if (propType != null)
                    {
                        var result = ReadObject(
                            assembly,
                            propType,
                            value,
                            prop.Name);
                        kvpList.AddRange(result);
                    }
                }
                
                kvpList.Add(new KeyValuePair<string, string>(
                    formatParentObjectName(parentObjectName, prop.Name), value?.ToString() ?? string.Empty));                
            }

            return kvpList;

            string formatParentObjectName(string? parentName, string propName) =>
                string.IsNullOrWhiteSpace(parentObjectName)
                ? propName : $"{parentObjectName}.{propName}";
        }
    }
}
