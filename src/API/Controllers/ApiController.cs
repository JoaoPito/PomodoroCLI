using Pomogotchi.Persistence;
using Pomogotchi.Application.Timer;
using Pomogotchi.Domain;
using Pomogotchi.Application.SoundPlayer;

namespace Pomogotchi.API.Controllers
{
    public class ApiController : ApiControllerBase
    {
        IConfigLoader _configLoader;

        IPlayer? _dingPlayer;

        public override event Action? EndTriggers;

        public ApiController(IConfigLoader config, ISessionTimer timer, IPlayer? dingPlayer = null) : base(timer)
        {
            _configLoader = config;
            this._dingPlayer = dingPlayer;
            SetupTimer();
        }

        void SetupTimer()
        {
            Timer.SetTrigger(OnSessionEnd);
        }

        void OnSessionEnd()
        {
            EndTriggers?.Invoke();
            StopSession();
            TryPlaySessionEndSound();
        }

        void LoadNextSession()
        {
            CurrentSession = LoadNextSessionParams(CurrentSession.Type);
        }

        public override void StartSession()
        {
            Timer.Start();
            InSession = true;
        }

        public override void StopSession()
        {
            Timer.Stop();
            InSession = false;
        }

        Session LoadNextSessionParams(Session.SessionType type)
        {
            switch (type)
            {
                case Session.SessionType.Work:
                    return BreakSession;

                case Session.SessionType.Break:
                    return WorkSession;

                default:
                    return WorkSession;
            }
        }

        bool TryPlaySessionEndSound()
        {
            try
            {
                _dingPlayer?.Play();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public override void LoadDefaultConfig()
        {
            WorkSession = _configLoader.LoadWorkSession();
            BreakSession = _configLoader.LoadBreakSession();

            CurrentSession = WorkSession;
        }

        public override void SetSessionDuration(Session.SessionType type, TimeSpan duration)
        {
            if(type == Session.SessionType.Work){
                WorkSession = new Session(duration, type);
            }
            else if(type == Session.SessionType.Break){
                BreakSession = new Session(duration, type);
            }
            else
                throw new NotImplementedException($"Unknown Session type value: '{type}'");

            UpdateCurrentSessionParams();
        }

        public override void SwitchSessionTo(Session.SessionType type)
        {
            CurrentSession = new Session(CurrentSession.Duration, type);
            UpdateCurrentSessionParams();
        }
    }
}