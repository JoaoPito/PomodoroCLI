using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pomogotchi.API.Controllers;
using Pomogotchi.Application.Timer;
using Pomogotchi.Domain;
using Pomogotchi.Persistence;

namespace Pomogotchi.API.Extensions.SessionExtension
{
    public class WorkSession : SessionType
    {
        public WorkSession(Session parameters, Action<Session>? paramsUpdate) : base(parameters, paramsUpdate)
        {
        }

        public override SessionType GetNextSession()
        {
            return new BreakSession(Parameters, _userOnParamsChanged);
        }

        public override void LoadConfig(IConfigExtension configLoader)
        {
            Parameters = configLoader.GetWorkParameters();
        }
    }
}