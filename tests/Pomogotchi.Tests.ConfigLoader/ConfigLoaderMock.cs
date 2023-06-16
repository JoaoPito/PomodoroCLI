using System.Text.Json;
using Pomogotchi.Application.ConfigLoader;
using Pomogotchi.Domain;

namespace Pomogotchi.Tests.ConfigLoader
{
    public class ConfigLoaderMock : IConfigLoader
    {
        public Exception? ThrowWhenLoadingConfig = null;

        public Dictionary<string, string> Parameters { get; protected set; } = new();

        public T? GetParamAs<T>(string key)
        {
            return JsonSerializer.Deserialize<T>(Parameters[key]);
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

        private void SetParam(string key, string value)
        {
            Parameters[key] = value;
        }

        public void SetParamAs<T>(string key, T data)
        {
            var serializedData = JsonSerializer.Serialize(data);
            SetParam(key, serializedData);
        }
    }
}