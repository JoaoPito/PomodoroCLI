using Pomogotchi.Persistence;
using Pomogotchi.Application.Timer;
using Pomogotchi.Domain;
using Pomogotchi.Application.SoundPlayer;

namespace Pomogotchi.API.Controllers
{
    public class SessionsController : ControllerBase
    {
        IConfigLoader _configLoader;

        IPlayer? _dingPlayer;

        public override event Action? EndTriggers;

        public SessionsController(IConfigLoader config, ISessionTimer timer, IPlayer? dingPlayer = null)
        {
            _configLoader = config;
            this._timer = timer;
            this._dingPlayer = dingPlayer;

            _currentSession = _configLoader.LoadWorkSession();
            InitTimer();
        }

        void InitTimer()
        {
            UpdateTimerDuration();
            _timer.SetTrigger(OnSessionEnd);
        }

        void OnSessionEnd()
        {
            EndTriggers?.Invoke();
            StopSession();
            TryPlaySessionEndSound();
        }

        public override void LoadNextSession()
        {
            _currentSession = LoadNextSessionParams(_currentSession.Type);
            _timer.SetDuration(_currentSession.Duration);
        }

        public override void StartSession()
        {
            _timer.Start();
            _inSession = true;
        }

        public override void StopSession()
        {
            _timer.Stop();
            _inSession = false;
        }

        Session LoadNextSessionParams(Session.SessionType type)
        {
            Session session;

            switch (type)
            {
                case Session.SessionType.Work:
                    session = _configLoader.LoadBreakSession();
                    break;

                case Session.SessionType.Break:
                    session = _configLoader.LoadWorkSession();
                    break;

                default:
                    session = _configLoader.LoadWorkSession();
                    break;
            }

            return session;
        }

        void TryPlaySessionEndSound()
        {
            try
            {
                _dingPlayer?.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void UpdateTimerDuration()
        {
            _timer.SetDuration(_currentSession.Duration);
        }

        void ChangeSessionDuration(TimeSpan amount)
        {
            if ((_currentSession.Duration + amount).TotalMinutes >= 5 && (_currentSession.Duration + amount).TotalHours < 24)
                _currentSession = new Session(_currentSession.Duration + amount, _currentSession.Type);

            UpdateTimerDuration();
        }

        public override void IncrementClock()
        {
            var incrementAmount = new TimeSpan(0, 5, 0);
            ChangeSessionDuration(incrementAmount);
        }

        public override void DecrementClock()
        {
            var decrementQuantity = new TimeSpan(0, -5, 0);
            ChangeSessionDuration(decrementQuantity);
        }
    }
}