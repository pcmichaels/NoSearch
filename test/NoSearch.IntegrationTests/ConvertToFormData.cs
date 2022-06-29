using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NoSearch.IntegrationTests
{
    public class ConvertToFormData
    {
        public static FormUrlEncodedContent ConvertToFormContent<T>(T toConvert)
        {            
            var kvpList = ReadObject(
                typeof(T).Assembly, 
                typeof(T), 
                toConvert);

            var formContent = new FormUrlEncodedContent(kvpList);
            return formContent;
        }

        private static List<KeyValuePair<string, string>> ReadObject(Assembly assembly, Type objectType, object toConvert)
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
                            value);
                    }
                }

                kvpList.Add(new KeyValuePair<string, string>(prop.Name, value!.ToString()));
            }

            return kvpList;
        }
    }
}
