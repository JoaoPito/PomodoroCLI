using ReactiveUI;
using PomodoroCLI.Timer;
using PomoGUI.Models;
using System;
using Avalonia.Controls;

namespace PomoGUI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        string _clock = "";
        public string Clock
        {
            get => _clock;
            set => this.RaiseAndSetIfChanged(ref _clock, value);
        }

        string _startStopButtonText = "Start";
        public string StartStopButtonText
        {
            get => _startStopButtonText;
            set => this.RaiseAndSetIfChanged(ref _startStopButtonText, value);
        }

        bool _inSession = false;
        public bool InSession {
            get => _inSession;
            set => this.RaiseAndSetIfChanged(ref _inSession, value);
        }

        int _sessionProgressPercent = 0;
        public int SessionProgressPercent
        {
            get => _sessionProgressPercent;
            set => this.RaiseAndSetIfChanged(ref _sessionProgressPercent, value);
        }

        SessionTimer timer;

        Session currentSession;

        public MainWindowViewModel()
        {
            var sessionDuration = LoadWorkDuration();

            timer = new SessionTimer(new SystemTimer());
            timer.SetDuration(sessionDuration);
            timer.RegisterUpdateTrigger(OnTimerUpdate);
            timer.SetTrigger(OnSessionEnd);

            currentSession = new Session(sessionDuration, Session.SessionType.None);

            UpdateTimerText();
            UpdateButtonText();
        }

        public void OnStartStopButton()
        {
            if (InSession)
            {
                StopSession();
            }
            else
            {
                StartNextSession();
            }
        }

        void OnTimerUpdate()
        {
            UpdateTimerText();
            UpdateSessionProgressBar();
        }

        void UpdateTimerText()
        {
            var remainingTime = timer.GetRemainingTime();
            Clock = String.Format("{0:D2}:{1:D2}:{2:D2}", remainingTime.Hours, remainingTime.Minutes, remainingTime.Seconds);
        }

        void UpdateSessionProgressBar()
        {
            SessionProgressPercent = (int)(Math.Round(timer.GetRemainingTime().TotalSeconds/currentSession.Duration.TotalSeconds * 100));
        }

        void UpdateButtonText()
        {
            if(InSession)
            {
                StartStopButtonText = "Skip";
            } 
            else if(currentSession.CurrentSession == Session.SessionType.Work) {
                StartStopButtonText = "Start Break";
            }
            else
            {
                StartStopButtonText = "Start";
            }
        }

        void StartNextSession()
        {
            switch (currentSession.CurrentSession)
            {
                case Session.SessionType.Work:
                    LoadBreakSessionConfig();
                    break;

                case Session.SessionType.Break:
                    LoadWorkSessionConfig();
                    break;

                default:
                    LoadWorkSessionConfig();
                    break;
            }

            timer.Start();
            InSession = true;
            UpdateButtonText();
        }

        void LoadWorkSessionConfig()
        {
            TimeSpan duration = LoadWorkDuration();
            currentSession = new Session(duration, Session.SessionType.Work);
            timer.SetDuration(duration);
        }

        void LoadBreakSessionConfig()
        {
            TimeSpan duration = LoadBreakDuration();
            currentSession = new Session(duration, Session.SessionType.Break);
            timer.SetDuration(duration);
        }

        void StopSession()
        {
            timer.Stop();
            InSession = false;
            UpdateButtonText();
        }

        void OnSessionEnd()
        {
            StopSession();
        }

        TimeSpan LoadWorkDuration()
        {
            return new TimeSpan(0, 1, 0);
        }

        TimeSpan LoadBreakDuration()
        {
            return new TimeSpan(0, 0, 15);
        }
    }
}