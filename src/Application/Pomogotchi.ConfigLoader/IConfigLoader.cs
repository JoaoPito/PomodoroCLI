using Pomogotchi.Domain;

namespace Pomogotchi.Application.ConfigLoader
{
    public interface IConfigLoader
    {
        public void ReloadConfig();
        public void SaveChanges();
        public Session GetWorkParams();
        public Session GetBreakParams();
        public void SetWorkParams(Session session);
        public void SetBreakParams(Session session);
        public string GetExtensionParam(string key);
        public void SetExtensionParam(string key, string value);
    }
}