using Pomogotchi.API.Controllers;
using Pomogotchi.API.Extensions.Notifications;
using Pomogotchi.Domain;

namespace Pomogotchi.API.Extensions
{
    public interface IConfigExtension : IAPIExtension
    {
        public T? GetParamAs<T>(string key);
        public void SetParamAs<T>(string key, T data);

        public CommandResult ReloadAllConfigs();

    }
}