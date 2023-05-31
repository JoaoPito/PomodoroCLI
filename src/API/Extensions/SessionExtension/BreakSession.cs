using Pomogotchi.API.Controllers;
using Pomogotchi.Application.Timer;
using Pomogotchi.Domain;
using Pomogotchi.Persistence;

namespace Pomogotchi.API.Extensions.SessionExtension
{
    public class BreakSession : SessionType

    {
        public BreakSession(Session parameters, Action<Session>? paramsUpdate) : base(parameters, paramsUpdate)
        {
        }

        public override SessionType GetNextSession()
        {
            return new WorkSession(Parameters, _userOnParamsChanged);
        }

        public override void LoadConfig(IConfigExtension configLoader)
        {
            Parameters = configLoader.GetBreakParameters();
        }
    }
}