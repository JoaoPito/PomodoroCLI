using Pomogotchi.Application.Timer;
using Pomogotchi.Domain;

namespace Pomogotchi.API.Controllers
{
    public abstract class ApiControllerBase
    {
        //protected IMediator _mediator;
        //public IMediator Mediator => _mediator;

        private bool _inSession = false;
        public bool InSession { get => _inSession; protected set => _inSession = value; }

        private ISessionTimer _timer;
        public ISessionTimer Timer { get => _timer; protected set => _timer = value; }

        private Session _currentSession = new(TimeSpan.Zero, Session.SessionType.Work);
        public Session CurrentSession { get => _currentSession; protected set { _currentSession = value; OnCurrentSessionChanged(value); } }

        private Session _workSession = new(TimeSpan.Zero, Session.SessionType.Work);
        public Session WorkSession { get => _workSession; protected set => _workSession = value; }

        private Session _breakSession = new(TimeSpan.Zero, Session.SessionType.Break);
        public Session BreakSession { get => _breakSession; protected set => _breakSession = value; }

        public ApiControllerBase(ISessionTimer timer)
        {
            this.Timer = timer;
        }

        public abstract event Action? EndTriggers;

        public abstract void LoadDefaultConfig();

        public abstract void StartSession();
        public abstract void StopSession();

        public abstract void SetSessionDuration(Session.SessionType type, TimeSpan duration);
        public abstract void SwitchSessionTo(Session.SessionType type);

        protected virtual void UpdateTimerParams()
        {
            _timer.SetDuration(CurrentSession.Duration);
        }

        protected virtual void OnCurrentSessionChanged(Session value)
        {
            UpdateTimerParams();
        }

        protected virtual void UpdateCurrentSessionParams()
        {
            if (CurrentSession.Type == Session.SessionType.Work) CurrentSession = WorkSession;
            else if (CurrentSession.Type == Session.SessionType.Break) CurrentSession = BreakSession;
        }
    }
}