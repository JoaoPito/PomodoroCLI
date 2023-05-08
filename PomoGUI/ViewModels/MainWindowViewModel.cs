using ReactiveUI;
using PomodoroCLI.Timer;
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

        string _interactButtonText = "Start";
        public string InteractButtonText
        {
            get => _interactButtonText;
            set => this.RaiseAndSetIfChanged(ref _interactButtonText, value);
        }

        bool _inSession = false;
        public bool InSession {
            get => _inSession;
            set => this.RaiseAndSetIfChanged(ref _inSession, value);
        }

        SessionTimer timer;

        TimeSpan _selectedDuration = new TimeSpan(00, 45, 00);
        public TimeSpan SelectedDuration {
            get => _selectedDuration;
            set => this.RaiseAndSetIfChanged(ref _selectedDuration, value);
        }

        public MainWindowViewModel()
        {
            timer = new SessionTimer(new SystemTimer());
            TimeSpan defaultSessionDuration = TimeSpan.FromMinutes(1);
            timer.SetDuration(defaultSessionDuration);
            timer.RegisterUpdateTrigger(OnTimerUpdate);

            UpdateTimerText();
        }

        public void StartSession()
        {
            timer.Start();
            InSession = true;
        }

        void OnTimerUpdate()
        {
            UpdateTimerText();
        }

        void UpdateTimerText()
        {
            var remainingTime = timer.GetRemainingTime();
            Clock = String.Format("{0:D2}:{1:D2}:{2:D2}", remainingTime.Hours, remainingTime.Minutes, remainingTime.Seconds);
        }
    }
}