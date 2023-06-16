using Pomogotchi.Application.ConfigLoader;
using Pomogotchi.Domain;

namespace Pomogotchi.Tests.ConfigLoader
{
    public class ConfigLoaderMock : IConfigLoader
    {
        public Exception? ThrowWhenLoadingConfig = null;

        public string GetParam(string key)
        {
            throw new NotImplementedException();
        }

        public void LoadDefaults()
        {
            
        }

        public void ReloadConfig()
        {
            if (ThrowWhenLoadingConfig != null)
                throw ThrowWhenLoadingConfig;
        }

        public void SaveChanges()
        {
            
        }

        public void SetParam(string key, string value)
        {
            throw new NotImplementedException();
        }
    }
}