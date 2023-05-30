using ReactiveUI;
using PomoGUI.Models;
using System;
using Avalonia.Controls;

namespace PomoGUI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        MainWindowController _controller;

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

        bool _canChangeSettings = true;
        public bool CanChangeSettings
        {
            get => _canChangeSettings;
            set => this.RaiseAndSetIfChanged(ref _canChangeSettings, value);
        }

        public MainWindowViewModel()
        {
            _controller = new MainWindowController(OnSessionEnd, OnTimerUpdate);
            UpdateTimerUI();
        }

        public void OnStartStopButton()
        {
            if (_controller.Controller.InSession)
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
            UpdateTimerText(_controller.Controller.Timer.GetRemainingTime());
            UpdateButtonText(_controller.Controller.CurrentSession.Type);
            UpdateSessionProgressBar(_controller.Controller.Timer.GetRemainingTime(), _controller.Controller.CurrentSession.Duration);
        }

        void UpdateTimerText(TimeSpan remainingTime)
        {
            Clock = String.Format("{0:D2}:{1:D2}:{2:D2}", remainingTime.Hours, remainingTime.Minutes, remainingTime.Seconds);
        }

        void UpdateSessionProgressBar(TimeSpan remaining, TimeSpan total)
        {
            SessionProgressPercent = (int)(Math.Round(remaining.TotalSeconds / total.TotalSeconds * 100));
        }

        void UpdateButtonText(Pomogotchi.Domain.Session.SessionType currentSessionType)
        {
            var TxtRepository = GUITextRepository.GetRepository();

            if (_controller.Controller.InSession)
            {
                StartStopButtonText = TxtRepository.SkipSessionTxt;
            }
            else
            {
                switch (currentSessionType)
                {
                    case Pomogotchi.Domain.Session.SessionType.Break:
                        StartStopButtonText = TxtRepository.StartBreakSessionTxt;
                        break;

                    case Pomogotchi.Domain.Session.SessionType.Work:
                    default:
                        StartStopButtonText = TxtRepository.StartWorkSessionTxt;
                        break;
                }
            }
        }

        void StartSession()
        {
            _controller.Controller.StartSession();
            CanChangeSettings = false;
            UpdateTimerUI();
        }

        void SkipSession()
        {
            OnSessionEnd();
        }

        void OnSessionEnd()
        {
            _controller.StopCurrentSession();
            _controller.LoadNextSession();
            CanChangeSettings = true;
            UpdateTimerUI();
        }

        public void OnIncrementButton()
        {
            _controller.IncrementClock();
            UpdateTimerUI();
        }

        public void OnDecrementButton()
        {
            _controller.DecrementClock();
            UpdateTimerUI();
        }
    }
}