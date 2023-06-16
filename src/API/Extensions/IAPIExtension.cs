using API;
using Pomogotchi.API.Extensions.Notifications;

namespace Pomogotchi.API.Extensions
{
    public interface IAPIExtension : IApiComponent
    {
        public class ExtensionNotFoundException : ArgumentException{}

        public Result Notify(GenericNotification notification);
    }
}