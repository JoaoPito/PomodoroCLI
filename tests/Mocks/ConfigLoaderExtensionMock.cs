using Pomogotchi.API.Extensions;
using Pomogotchi.API.Extensions.Notifications;
using Pomogotchi.Domain;

namespace Pomogotchi.Tests.Mocks
{
    public class ConfigLoaderExtensionMock : IConfigExtension
    {
        private Dictionary<string,string> _params = new();

        public Dictionary<string,string> Params => _params;

        public ConfigLoaderExtensionMock()
        {
            
        }

        public ConfigLoaderExtensionMock(Dictionary<string,string> config)
        {
            this._params = config;
        }

        public string GetParam(string key)
        {
            return Params[key];
        }

        public CommandResult Notify(GenericNotification notification)
        {
            throw new NotImplementedException();
        }

        public void SetParam(string key, string data)
        {
            throw new NotImplementedException();
        }
        public CommandResult ReloadAllConfigs()
        {
            throw new NotImplementedException();
        }

        public T GetParamAs<T>(string key)
        {
            throw new NotImplementedException();
        }

        public void SetParamAs<T>(string key, T data)
        {
            throw new NotImplementedException();
        }
    }
}