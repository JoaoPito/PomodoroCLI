using Pomogotchi.Timer;
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
        public bool InSession { get => _inSession;}
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
            UpdateTimerDuration();
            timer.SetTrigger(OnSessionEnd);
        }

        void OnSessionEnd()
        {
            EndTriggers?.Invoke();
            StopSession();
            TryPlaySessionEndSound();
            
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

        void TryPlaySessionEndSound()
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

        void UpdateTimerDuration()
        {
            timer.SetDuration(currentSession.Duration);
        }

        void ChangeSessionDuration(TimeSpan amount)
        {
            if ((currentSession.Duration + amount).TotalMinutes >= 5 && (currentSession.Duration + amount).TotalHours < 24)
                currentSession = new SessionParams(currentSession.Duration + amount, currentSession.Type);

            UpdateTimerDuration();
        }

        public void IncrementClock()
        {
            var incrementAmount = new TimeSpan(0, 5, 0);
            ChangeSessionDuration(incrementAmount);
        }

        public void DecrementClock()
        {
            var decrementQuantity = new TimeSpan(0, -5, 0);
            ChangeSessionDuration(decrementQuantity);
        }
    }
}
