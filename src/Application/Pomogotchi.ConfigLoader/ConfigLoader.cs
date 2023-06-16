using Pomogotchi.Domain;
using Pomogotchi.Persistence;

namespace Pomogotchi.Application.ConfigLoader
{
    public class ConfigLoader : IConfigLoader
    {
        static ConfigLoader? _instance;

        const string DEFAULT_CONFIG_FILE_PATH = "./config.json";
        private ConfigParams _currentParams = new();
        private JsonFileHandler _json;

        private ConfigLoader(string? configFilePath) {

            var validator = new FileHandler.ConfigFileValidator();

            if(configFilePath != null && validator.Validate(configFilePath).IsValid)
                _json = new JsonFileHandler(configFilePath);
            else
                _json = new JsonFileHandler(DEFAULT_CONFIG_FILE_PATH);

            ReloadConfig();
        }

        public static ConfigLoader GetController(string? configFilePath)
        {
            if(_instance == null)
                _instance = new ConfigLoader(configFilePath);

            return _instance;
        }

        public static ConfigLoader GetController()
        {
            if(_instance == null)
                _instance = new ConfigLoader(null);

            return _instance;
        }

        public void ReloadConfig()
        {
            _currentParams = _json.LoadFromFile<ConfigParams>();           
        }

        public void LoadDefaults(){
            _currentParams = new ConfigParams{ WorkDuration = new TimeSpan(0,25,0), BreakDuration = new TimeSpan(0,5,0) };
        }

        public string GetParam(string key)
        {
            if(!_currentParams.Extensions.ContainsKey(key))
                throw new ArgumentException("Requested key not found");

            Console.WriteLine($"param: {_currentParams.Extensions[key]} type: {_currentParams.Extensions[key].GetType()}");

            return _currentParams.Extensions[key];
        }

        public void SetParam(string key, string value)
        {
            _currentParams.Extensions.Add(key, value);
        }

        public void SaveChanges()
        {
            _json.SaveToFile<ConfigParams>(_currentParams);
        }
    }
}