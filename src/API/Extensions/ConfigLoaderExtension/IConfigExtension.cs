using Pomogotchi.API.Controllers;
using Pomogotchi.API.Extensions.Notifications;
using Pomogotchi.Domain;

namespace Pomogotchi.API.Extensions
{
    public interface IConfigExtension : IAPIExtension
    {
        public void SetParam(string key, string data);
        public string GetParam(string key);
        public T GetParamAs<T>(string key);

        public CommandResult LoadConfig();

    }
}