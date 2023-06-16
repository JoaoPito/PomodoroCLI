using Pomogotchi.Domain;

namespace Pomogotchi.API.Extensions
{
    public interface IConfigExtension : IAPIExtension
    {
        public Session GetWorkParameters();
        public Session GetBreakParameters();

        public void SetWorkParams(Session parameters);
        public void SetBreakParams(Session parameters);

        public void SetExtensionParam(string key, string data);
        public string GetExtensionParam(string key);

    }
}