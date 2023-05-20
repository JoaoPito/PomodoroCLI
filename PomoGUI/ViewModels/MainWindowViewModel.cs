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
        ConfigController configLoader;

        public MainWindowViewModel()
        {
            configLoader = ConfigController.GetController();

            currentSession = configLoader.LoadWorkSession();

            InitTimer();

            UpdateTimerText();
            UpdateButtonText();
        }

        void InitTimer()
        {
            timer = new SessionTimer(new SystemTimer());
            timer.SetDuration(currentSession.Duration);
            timer.RegisterUpdateTrigger(OnTimerUpdate);
            timer.SetTrigger(OnSessionEnd);
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
            var TxtRepository = GUITextRepository.GetRepository();

            if (InSession)
            {
                StartStopButtonText = TxtRepository.SkipSessionTxt;
            }
            else
            {
                switch (currentSession.Type)
                {
                    case Session.SessionType.Break:
                        StartStopButtonText = TxtRepository.StartBreakSessionTxt;
                        break;

                    case Session.SessionType.Work:
                    default:
                        StartStopButtonText = TxtRepository.StartWorkSessionTxt;
                        break;
                }
            }
        }

        void StartNextSession()
        {
            switch (currentSession.Type)
            {
                case Session.SessionType.Work:
                    currentSession = configLoader.LoadBreakSession();
                    break;

                case Session.SessionType.Break:
                    currentSession = configLoader.LoadWorkSession();
                    break;

                default:
                    currentSession = configLoader.LoadWorkSession();
                    break;
            }

            timer.SetDuration(currentSession.Duration);
            timer.Start();
            InSession = true;
            UpdateButtonText();
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
    }
}