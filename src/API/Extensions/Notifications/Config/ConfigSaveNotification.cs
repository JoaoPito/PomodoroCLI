using API;

namespace Pomogotchi.API.Extensions.Notifications
{
    public class ConfigSaveNotification : GenericNotification
    {
        public ConfigSaveNotification(IApiComponent context) : base(context)
        {
        }
    }
}