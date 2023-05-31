using Pomogotchi.API.Extensions;
using Pomogotchi.Domain;

namespace Pomogotchi.API.Extensions.SessionExtension
{
    public abstract class SessionType
    {
        private Session _parameters;
        public Session Parameters { get => _parameters; protected set { _parameters = value; OnParametersChanged(value); }}

        protected Action<Session>? _userOnParamsChanged;
        public SessionType(Session parameters, Action<Session>? onParamsChanged)
        {
            this._userOnParamsChanged = onParamsChanged;
            this._parameters = parameters;
        }
        private void OnParametersChanged(Session value)
        {
            if(_userOnParamsChanged != null) _userOnParamsChanged(value);
        }

        public abstract void LoadConfig(IConfigExtension configLoader);
        public abstract SessionType GetNextSession();

    }
}