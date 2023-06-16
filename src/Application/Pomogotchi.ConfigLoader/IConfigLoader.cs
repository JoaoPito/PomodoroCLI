using Pomogotchi.Domain;

namespace Pomogotchi.Application.ConfigLoader
{
    public interface IConfigLoader
    {
        public void ReloadConfig();
        public void LoadDefaults();
        public void SaveChanges();
        public string GetParam(string key);
        public void SetParam(string key, string value);
    }
}