using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API;

namespace Pomogotchi.API.Extensions.Notifications
{
    public class SessionEndNotification : GenericNotification
    {
        public SessionEndNotification(IApiComponent context) : base(context)
        {
        }
    }
}