using Pomogotchi.API.Extensions;
using Pomogotchi.Domain;

namespace Pomogotchi.API.Extensions.SessionExtension
{
    public abstract class SessionType
    {
        private Session _parameters;
        public Session Parameters { get => _parameters; protected set { _parameters = value; OnParametersChanged(value); }}

        public Session.SessionType Type { get => Parameters.Type; }
        public TimeSpan Duration {get => Parameters.Duration; }

        protected Action<Session>? _userOnParamsChanged;
        public SessionType(Session parameters){
            this._userOnParamsChanged = null;
            this._parameters = parameters;
        }
        public SessionType(Session parameters, Action<Session>? onParamsChanged)
        {
            this._userOnParamsChanged = onParamsChanged;
            this._parameters = parameters;
        }
        private void OnParametersChanged(Session value)
        {
            if(_userOnParamsChanged != null) _userOnParamsChanged(value);
        }
        public void SetOnParamsChangedAction(Action<Session> action){
            _userOnParamsChanged = action;
        }

        public abstract void LoadConfig(IConfigExtension configLoader);
        public abstract SessionType GetNextSession();

    }
}