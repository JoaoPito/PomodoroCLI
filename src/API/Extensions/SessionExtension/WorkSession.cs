using Pomogotchi.Domain;

namespace Pomogotchi.API.Extensions.SessionExtension
{
    public class WorkSession : SessionType
    {

        const string WORK_SESSION_CONFIG = "work_session";
        public WorkSession(Session parameters, Action<Session>? onParamsChanged) : base(parameters, onParamsChanged){}

        public WorkSession(Session parameters) : base(parameters){}

        public override SessionType GetNextSession()
        {
            return new BreakSession(Parameters, _userOnParamsChanged);
        }

        public override void LoadConfig(IConfigExtension configLoader)
        {
            Parameters = configLoader.GetParamAs<Session>(WORK_SESSION_CONFIG);
        }
    }
}