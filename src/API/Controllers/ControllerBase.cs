using Pomogotchi.Application.Timer;
using Pomogotchi.Domain;

namespace Pomogotchi.API.Controllers
{
    public abstract class ControllerBase
    {
        //protected IMediator _mediator;
        //public IMediator Mediator => _mediator;

        protected bool _inSession = false;
        public bool InSession { get => _inSession;}

        protected ISessionTimer _timer;
        public ISessionTimer Timer { get => _timer; }
        protected Session _currentSession;
        public Session CurrentSession { get => _currentSession; }
        public abstract event Action? EndTriggers;

        public abstract void StartSession();
        public abstract void StopSession();
        public abstract void LoadNextSession();

        public abstract void IncrementClock();
        public abstract void DecrementClock();
    }
}