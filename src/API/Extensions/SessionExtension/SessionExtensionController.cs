using Pomogotchi.API.Controllers;
using Pomogotchi.API.Extensions.Notifications;
using Pomogotchi.API.Extensions.SessionExtension;
using Pomogotchi.Application.Timer;
using Pomogotchi.Domain;

namespace Pomogotchi.API.Extensions
{
    public class SessionExtensionController : IAPIExtension
    {
        private ApiControllerBase _controller;

        private bool _inSession = false;
        public bool InSession { get => _inSession; protected set => _inSession = value; }

        private ISessionTimer _timer;
        public ISessionTimer Timer { get => _timer; protected set => _timer = value; }

        private SessionType _session;
        public SessionType Session { get => _session; protected set{_session = value;} } 
        public TimeSpan Duration { get => Session.Duration; }

        public event Action? EndTriggers;

        public SessionExtensionController(ApiControllerBase controller, ISessionTimer timer)
        {
             _session = new WorkSession(new Domain.Session(new TimeSpan(0, 25, 0)));
            this._controller = controller;
            this._timer = timer;
            _controller.GetConfigLoader().ReloadAllConfigs();

            SetupTimer();
        }
        void SetupTimer()
        {
            Timer.SetTrigger(OnTimerEnd);
            Timer.RegisterUpdateTrigger(OnTimerUpdate);
            UpdateTimerParams(Session.Parameters);
        }
        public void Start()
        {
            Timer.Start();
            InSession = true;
        }
        public void Stop()
        {
            Timer.Stop();
            InSession = false;
        }

        void OnTimerEnd(){
            Stop();
            EndTriggers?.Invoke();
            _controller.NotifyAllExtensions(new SessionEndNotification(this));
        }

        void OnTimerUpdate(){
            _controller.NotifyAllExtensions(new SessionUpdateNotification(this));
        }

        protected virtual void OnSessionParamsChanged(Session value)
        {
            UpdateTimerParams(value);
        }

        protected virtual void UpdateTimerParams(Session parameters)
        {
            Timer.SetDuration(parameters.Duration);
        }

        public CommandResult Notify(GenericNotification notification)
        {
            if(notification.GetType() == typeof(SessionEndNotification))
                Stop();
            if(notification.GetType() == typeof(ConfigLoadNotification))
                LoadConfig();

            return CommandResult.Success();
        }

        private void LoadConfig()
        {
            throw new NotImplementedException();
        }

        public void SwitchSessionTo(SessionType session)
        {
            Session = session;
            Session.SetOnParamsChangedAction(OnSessionParamsChanged);
            OnSessionParamsChanged(Session.Parameters);
        }
    }
}
