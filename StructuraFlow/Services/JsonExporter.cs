using StructuraFlow.Models;
using Newtonsoft.Json;

namespace StructuraFlow.Services
{
    public class JsonExporter
    {
        public string ExportToJson(OutputModel data, string filePath)
        {
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);

            File.WriteAllText(filePath, json);

            return json;
        }
    }
}
