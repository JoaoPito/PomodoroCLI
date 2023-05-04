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
        }

        public void StartSession()
        {
            timer.Start();
        }
    }
}