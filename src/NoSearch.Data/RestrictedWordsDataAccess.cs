using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NoSearch.Data
{
    public class RestrictedWordsDataAccess : IRestrictedWordsDataAccess
    {
        public IEnumerable<string> GetAll()
        {
            string executingDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
            string jsonData = File.ReadAllText($"{executingDir}/Data/BadWords.json");

            var options = new JsonSerializerOptions
            {
                ReadCommentHandling = JsonCommentHandling.Skip
            };
            var restrictedWords = JsonSerializer.Deserialize<IEnumerable<string>>(
                jsonData, options);

            if (restrictedWords == null)
            {
                throw new Exception("No data file found");
            }

            return restrictedWords;
        }

    }
}
