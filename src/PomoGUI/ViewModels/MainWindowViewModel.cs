using ReactiveUI;
using PomodoroCLI.Timer;
using PomoGUI.Models;
using System;
using Avalonia.Controls;
using Pomogotchi.SoundPlayer;

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

        int _sessionProgressPercent = 0;
        public int SessionProgressPercent
        {
            get => _sessionProgressPercent;
            set => this.RaiseAndSetIfChanged(ref _sessionProgressPercent, value);
        }

        SessionController controller;

        bool _canChangeSettings = true;
        public bool CanChangeSettings { 
            get => _canChangeSettings;
            set => this.RaiseAndSetIfChanged(ref _canChangeSettings, value);
        }

        public MainWindowViewModel()
        {
            var timer = new SessionTimer(new SystemTimer());
            var dingPlayer = new SFXPlayer("./ding.wav");
            controller = new SessionController(ConfigLoader.GetController(), timer, dingPlayer);
            controller.EndTriggers += OnSessionEnd;
            controller.Timer.RegisterUpdateTrigger(OnTimerUpdate);

            UpdateTimerUI();
        }

        public void OnStartStopButton()
        {
            if (controller.InSession)
            {
                SkipSession();
            }
            else
            {
                StartSession();
            }
                
        }

        void OnTimerUpdate()
        {
            UpdateTimerUI();
        }

        void UpdateTimerUI()
        {
            UpdateTimerText(controller.Timer.GetRemainingTime());
            UpdateButtonText(controller.CurrentSession.Type);
            UpdateSessionProgressBar(controller.Timer.GetRemainingTime(), controller.CurrentSession.Duration);
        }

        void UpdateTimerText(TimeSpan remainingTime)
        {
            Clock = String.Format("{0:D2}:{1:D2}:{2:D2}", remainingTime.Hours, remainingTime.Minutes, remainingTime.Seconds);
        }

        void UpdateSessionProgressBar(TimeSpan remaining, TimeSpan total)
        {
            SessionProgressPercent = (int)(Math.Round(remaining.TotalSeconds / total.TotalSeconds * 100));
        }

        void UpdateButtonText(SessionParams.SessionType currentSessionType)
        {
            var TxtRepository = GUITextRepository.GetRepository();

            if (controller.InSession)
            {
                StartStopButtonText = TxtRepository.SkipSessionTxt;
            }
            else
            {
                switch (currentSessionType)
                {
                    case SessionParams.SessionType.Break:
                        StartStopButtonText = TxtRepository.StartBreakSessionTxt;
                        break;

                    case SessionParams.SessionType.Work:
                    default:
                        StartStopButtonText = TxtRepository.StartWorkSessionTxt;
                        break;
                }
            }
        }

        void StartSession()
        {
            controller.StartSession();
            CanChangeSettings = false;
            UpdateTimerUI();
        }

        void SkipSession()
        {
            OnSessionEnd();
        }

        void OnSessionEnd()
        {
            controller.StopSession();
            controller.LoadNextSession();
            CanChangeSettings = true;
            UpdateTimerUI();
        }

        public void OnIncrementButton()
        {
            controller.IncrementClock();
            UpdateTimerUI();
        }

        public void OnDecrementButton()
        {
            controller.DecrementClock();
            UpdateTimerUI();
        }
    }
}