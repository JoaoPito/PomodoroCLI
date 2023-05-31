using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API;

namespace Pomogotchi.API.Extensions.Notifications
{
    public class SessionUpdateNotification : GenericNotification
    {
        public SessionUpdateNotification(IApiComponent context) : base(context)
        {
        }
    }
}