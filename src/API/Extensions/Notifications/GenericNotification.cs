using API;

namespace Pomogotchi.API.Extensions.Notifications
{
    public class GenericNotification
    {
        public readonly IApiComponent Context;

        public GenericNotification(IApiComponent context)
        {
            this.Context = context;
        }
    }
}