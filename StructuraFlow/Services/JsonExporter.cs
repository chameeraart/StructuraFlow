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



        public string ExportToJsonErrors(List<string> data, string filePath)
        {
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);

            var folder = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            File.WriteAllText(filePath, json);

            return json;
        }
    }
}


