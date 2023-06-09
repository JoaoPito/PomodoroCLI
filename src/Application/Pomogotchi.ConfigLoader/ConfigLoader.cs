using Pomogotchi.Domain;
using Pomogotchi.Persistence;

namespace Pomogotchi.Application.ConfigLoader
{
    public class ConfigLoader : IConfigLoader
    {
        static ConfigLoader? _instance;

        const string CONFIG_FILE_PATH = "./config.json";
        private ConfigParams _currentParams;
        private JsonFileHandler _json;

        private ConfigLoader() {
            _json = new JsonFileHandler(CONFIG_FILE_PATH);

            ReloadConfig();
        }

        public static ConfigLoader GetController()
        {
            if(_instance == null)
                _instance = new ConfigLoader();

            return _instance;
        }

        public void ReloadConfig()
        {
            try
            {
                _currentParams = _json.LoadFromFile<ConfigParams>();
            }
            catch (System.IO.FileNotFoundException)
            {
                _currentParams = LoadDefaults();
                SaveChanges();
            }
            
        }

        ConfigParams LoadDefaults(){
            return new ConfigParams{ WorkDuration = new TimeSpan(0,25,0), BreakDuration = new TimeSpan(0,5,0) };
        }

        public Session GetBreakParams()
        {
            return new Session(_currentParams.BreakDuration);
        }

        public Session GetWorkParams()
        {
            return new Session(_currentParams.WorkDuration);
        }

        public void SetWorkParams(Session session)
        {
            _currentParams.WorkDuration = session.Duration;
        }

        public void SetBreakParams(Session session)
        {
            _currentParams.BreakDuration = session.Duration;
        }

        public T GetExtensionParam<T>(string key)
        {
            throw new NotImplementedException();
        }

        public void SetExtensionParam<T>(string key, T value)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            _json.SaveToFile<ConfigParams>(_currentParams);
        }
    }
}