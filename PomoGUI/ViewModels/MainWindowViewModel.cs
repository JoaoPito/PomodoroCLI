using ReactiveUI;
using PomodoroCLI.Timer;
using System;

namespace PomoGUI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        string _clock;
        public string Clock
        {
            get => _clock;
            set => this.RaiseAndSetIfChanged(ref _clock, value);
        }

        SessionTimer timer;

        public MainWindowViewModel()
        {
            timer = new SessionTimer(new SystemTimer());
            TimeSpan defaultSessionDuration = TimeSpan.FromMinutes(45);
            timer.SetDuration(defaultSessionDuration);
            timer.RegisterUpdateTrigger(OnTimerUpdate);
        }

        public void StartSession()
        {
            timer.Start();
        }

        void OnTimerUpdate()
        {
            var remainingTime = timer.GetRemainingTime();
            Clock = String.Format("{0:D2}:{1:D2}:{2:D2}", remainingTime.Hours, remainingTime.Minutes, remainingTime.Seconds);
        }
    }
}