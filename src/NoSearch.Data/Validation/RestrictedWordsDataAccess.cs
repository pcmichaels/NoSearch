using System.Reflection;
using System.Text.Json;

namespace NoSearch.Data.Validation
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
