using Pomogotchi.Domain;
using Pomogotchi.Persistence;

namespace Pomogotchi.Application.ConfigLoader
{
    public class ConfigLoader : IConfigLoader
    {
        static ConfigLoader? _instance;

        const string DEFAULT_CONFIG_FILE_PATH = "./config.json";
        private Dictionary<string, string> _currentParams = new();
        private JsonFileHandler _json;

        private ConfigLoader(string? configFilePath) {

            var validator = new FileHandler.ConfigFileValidator();

            if(configFilePath != null && validator.Validate(configFilePath).IsValid)
                _json = new JsonFileHandler(configFilePath);
            else {
                _json = new JsonFileHandler(DEFAULT_CONFIG_FILE_PATH);
                _json.SaveToFile<Dictionary<string, string>>(_currentParams);
            }
                

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
            _currentParams = _json.LoadFromFile<Dictionary<string,string>>();           
        }

        public void LoadDefaults(){
            _currentParams = new Dictionary<string, string>{ };
        }

        string GetParam(string key)
        {
            if(!_currentParams.ContainsKey(key))
                throw new ArgumentException("Requested key not found");

            return _currentParams[key];
        }

        void SetParam(string key, string value)
        {
            _currentParams.Add(key, value);
        }

        public void SaveChanges()
        {
            _json.SaveToFile<Dictionary<string,string>>(_currentParams);
        }

        public T? GetParamAs<T>(string key)
        {
            string paramValue = GetParam(key);
            return _json.Deserialize<T>(paramValue);
        }

        public void SetParamAs<T>(string key, T data)
        {
            string serializedData = _json.Serialize<T>(data);
            SetParam(key, serializedData);
        }
    }
}