using Pomogotchi.API.Controllers;
using Pomogotchi.API.Extensions.Notifications;
using Pomogotchi.API.Extensions.SessionExtension;
using Pomogotchi.Application.Timer;
using Pomogotchi.Domain;
using Pomogotchi.Persistence;

namespace Pomogotchi.API.Extensions
{
    public class SessionExtensionController : IAPIExtension
    {
        private ApiControllerBase _controller;
        public ApiControllerBase Controller { get => _controller; protected set => _controller = value; }

        private bool _inSession = false;
        public bool InSession { get => _inSession; protected set => _inSession = value; }

        private ISessionTimer _timer;
        public ISessionTimer Timer { get => _timer; protected set => _timer = value; }

        private SessionType _session;
        public SessionType Session { get => _session; protected set{_session = value;} } 

        public event Action? EndTriggers;

        public SessionExtensionController(ApiControllerBase controller, ISessionTimer timer)
        {
            this._controller = controller;
            this._timer = timer;
            var sessionParams = new Domain.Session(new TimeSpan(0,15,0), Domain.Session.SessionType.Work);
            this.Session = new WorkSession(sessionParams, OnSessionParamsChanged);

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
            Controller.Notify(new SessionEndNotification(this));
        }

        void OnTimerUpdate(){
            Controller.Notify(new SessionUpdateNotification(this));
        }

        protected virtual void OnSessionParamsChanged(Session value)
        {
            UpdateTimerParams(value);
        }

        protected virtual void UpdateTimerParams(Session parameters)
        {
            Timer.SetDuration(parameters.Duration);
        }

        public void Notify(GenericNotification notification)
        {
            if(notification.GetType() == typeof(SessionEndNotification))
                Stop();
        }

        public void SwitchSessionTo(SessionType session)
        {
            Session = session;
        }
    }
}
