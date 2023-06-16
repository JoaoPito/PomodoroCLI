using ReactiveUI;
using PomoGUI.Models;
using System;
using Pomogotchi.API.Extensions.SessionExtension;

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
            if (_controller.InSession)
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
            UpdateTimerText(_controller.RemainingTime);
            UpdateButtonText(_controller.InSession, _controller.CurrentSession);
            UpdateSessionProgressBar(_controller.RemainingTime, _controller.TotalDuration);
        }

        void UpdateTimerText(TimeSpan remainingTime)
        {
            Clock = String.Format("{0:D2}:{1:D2}:{2:D2}", remainingTime.Hours, remainingTime.Minutes, remainingTime.Seconds);
        }

        void UpdateSessionProgressBar(TimeSpan remaining, TimeSpan total)
        {
            SessionProgressPercent = (int)(Math.Round(remaining.TotalSeconds / total.TotalSeconds * 100));
        }

        void UpdateButtonText(bool InSession, SessionType currentSession)
        {
            var txtRepository = GUITextRepository.GetRepository();

            if (InSession)
            {
                StartStopButtonText = txtRepository.SkipSessionTxt;
            }
            else
            {
                UpdateButtonWithSessionText(txtRepository, currentSession);
                
            }
        }

        void UpdateButtonWithSessionText(GUITextRepository txtRepository, SessionType currentSession){
            if(currentSession.GetType() == typeof(BreakSession))
                StartStopButtonText = txtRepository.StartBreakSessionTxt;
            else
                StartStopButtonText = txtRepository.StartWorkSessionTxt;
        }

        void StartSession()
        {
            _controller.StartSession();
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
            _controller.AddTimeToSessionDuration(new TimeSpan(0,5,0));
            _controller.SaveConfig();
            UpdateTimerUI();            
        }

        public void OnDecrementButton()
        {
            _controller.AddTimeToSessionDuration(new TimeSpan(0,-5,0));
            _controller.SaveConfig();
            UpdateTimerUI();
        }
    }
}