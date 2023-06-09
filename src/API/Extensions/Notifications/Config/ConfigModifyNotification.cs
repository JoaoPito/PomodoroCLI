using API;

namespace Pomogotchi.API.Extensions.Notifications
{
    public class ConfigModifyNotification : GenericNotification
    {
        public ConfigModifyNotification(IApiComponent context) : base(context)
        {
        }
    }
}