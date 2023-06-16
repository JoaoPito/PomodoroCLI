using Pomogotchi.API.Extensions;
using Pomogotchi.API.Extensions.Notifications;
using Pomogotchi.Domain;

namespace Pomogotchi.Tests.Mocks
{
    public class ConfigLoaderExtensionMock : IConfigExtension
    {
        private ConfigParams _params;

        public ConfigParams Params => _params;

        public ConfigLoaderExtensionMock()
        {
            _params = new ConfigParams();
        }

        public ConfigLoaderExtensionMock(ConfigParams config)
        {
            this._params = config;
        }

        public string GetParam(string key)
        {
            return Params.Extensions[key];
        }

        public CommandResult Notify(GenericNotification notification)
        {
            throw new NotImplementedException();
        }

        public void SetParam(string key, string data)
        {
            throw new NotImplementedException();
        }
        public CommandResult LoadConfig()
        {
            throw new NotImplementedException();
        }

        public T GetParamAs<T>(string key)
        {
            throw new NotImplementedException();
        }
    }
}