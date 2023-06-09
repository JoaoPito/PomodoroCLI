using API;

namespace Pomogotchi.API.Extensions.Notifications
{
    public class ConfigLoadNotification : GenericNotification
    {
        public ConfigLoadNotification(IApiComponent context) : base(context)
        {
        }
    }
}