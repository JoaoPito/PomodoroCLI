using PomodoroCLI.Timer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomoGUI.Models
{
    internal class SessionController
    {
        bool _inSession = false;
        public bool InSession { get => _inSession; }
        SessionTimer timer;
        public SessionTimer Timer { get => timer; }
        SessionParams currentSession;
        public SessionParams CurrentSession { get => currentSession; }
        public event Action? EndTriggers;
        ConfigController configLoader;

        public SessionController(){
            configLoader = ConfigController.GetController();
            currentSession = configLoader.LoadWorkSession();
            InitTimer();
        }

        void InitTimer()
        {
            timer = new SessionTimer(new SystemTimer());
            timer.SetDuration(currentSession.Duration);
            timer.SetTrigger(OnSessionEnd);
        }

        void OnSessionEnd()
        {
            EndTriggers?.Invoke();
            StopSession();
        }

        public void LoadNextSession()
        {
            currentSession = LoadNextSessionParams(currentSession.Type);
            timer.SetDuration(currentSession.Duration);
        }

        public void StartSession()
        {
            timer.Start();
            _inSession = true;
        }

        public void StopSession()
        {
            timer.Stop();
            _inSession = false;
        }

        SessionParams LoadNextSessionParams(SessionParams.SessionType type)
        {
            SessionParams session;

            switch (type)
            {
                case SessionParams.SessionType.Work:
                    session = configLoader.LoadBreakSession();
                    break;

                case SessionParams.SessionType.Break:
                    session = configLoader.LoadWorkSession();
                    break;

                default:
                    session = configLoader.LoadWorkSession();
                    break;
            }

            return session;
        }

    }
}
