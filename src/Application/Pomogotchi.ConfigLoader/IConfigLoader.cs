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
        public T GetExtensionParam<T>(string key);
        public void SetExtensionParam<T>(string key, T value);
    }
}