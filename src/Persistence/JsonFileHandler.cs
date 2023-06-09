using System.Text.Json;

namespace Pomogotchi.Persistence
{
    public class JsonFileHandler
    {
        private readonly string _filePath;
        public JsonFileHandler(string filePath)
        {
            this._filePath = filePath;
        }
        
        public void SaveToFile<T>(T obj){
            var options = new JsonSerializerOptions{ WriteIndented = true };
            string data = JsonSerializer.Serialize(obj, options);
            File.WriteAllText(_filePath, data);
        }

        public T LoadFromFile<T>(){
            string fileData = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<T>(fileData);
        }
    }
}