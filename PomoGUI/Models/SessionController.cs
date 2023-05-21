using PomodoroCLI.Timer;
using Pomogotchi.SoundPlayer;
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
        ISessionTimer timer;
        public ISessionTimer Timer { get => timer; }
        SessionParams currentSession;
        public SessionParams CurrentSession { get => currentSession; }
        public event Action? EndTriggers;
        IConfigLoader configLoader;

        IPlayer dingPlayer;

        public SessionController(IConfigLoader config, ISessionTimer timer, IPlayer dingPlayer)
        {
            configLoader = config;
            this.timer = timer;
            this.dingPlayer = dingPlayer;

            currentSession = configLoader.LoadWorkSession();
            InitTimer();
        }

        void InitTimer()
        {
            timer.SetDuration(currentSession.Duration);
            timer.SetTrigger(OnSessionEnd);
        }

        void OnSessionEnd()
        {
            EndTriggers?.Invoke();
            StopSession();
            TryToPlaySessionEndSound();
            
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

        void TryToPlaySessionEndSound()
        {
            try
            {
                dingPlayer.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
