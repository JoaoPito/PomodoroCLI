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
            
        }

        public ConfigLoaderExtensionMock(ConfigParams config)
        {
            this._params = config;
        }

        public Session GetBreakParameters()
        {
            throw new NotImplementedException();
        }

        public string GetExtensionParam(string key)
        {
            return Params.Extensions[key];
        }

        public Session GetWorkParameters()
        {
            throw new NotImplementedException();
        }

        public Result Notify(GenericNotification notification)
        {
            throw new NotImplementedException();
        }

        public void SetBreakParams(Session parameters)
        {
            throw new NotImplementedException();
        }

        public void SetExtensionParam(string key, string data)
        {
            throw new NotImplementedException();
        }

        public void SetWorkParams(Session parameters)
        {
            throw new NotImplementedException();
        }
    }
}