using Pomogotchi.API.Controllers;
using System;
using Pomogotchi.API.Builders;
using Pomogotchi.Domain;

namespace PomoGUI.Models
{
    public class MainWindowController
    {
        ApiControllerBase _controller;
        public ApiControllerBase Controller => _controller;

        public MainWindowController(Action? onSessionEnd, Action? onTimerUpdate)
        {
            BuildAPI();

            _controller.EndTriggers += onSessionEnd;
            if (onTimerUpdate != null) _controller.Timer.RegisterUpdateTrigger(onTimerUpdate);
        }

        void BuildAPI()
        {
            var builder = new SessionsControllerBuilder();
            builder.AddSoundPlayer();
            _controller = builder.GetController();
        }

        bool ValidateTimerDuration(TimeSpan amount)
        {
            return (Controller.CurrentSession.Duration + amount).TotalMinutes >= 5 && (Controller.CurrentSession.Duration + amount).TotalHours < 24;
        }

        void AddTimeToSessionDuration(TimeSpan amount)
        {
            if (ValidateTimerDuration(amount))
                Controller.SetSessionDuration(Controller.CurrentSession.Type, Controller.CurrentSession.Duration + amount);
        }

        public void StopCurrentSession()
        {
            Controller.StopSession();
        }

        public void LoadNextSession()
        {
            if (Controller.CurrentSession.Type == Session.SessionType.Work)
                Controller.SwitchSessionTo(Session.SessionType.Break);
            else
                Controller.SwitchSessionTo(Session.SessionType.Work);
        }

        public void DecrementClock()
        {
            AddTimeToSessionDuration(new TimeSpan(0, -5, 0));
        }

        public void IncrementClock()
        {
            AddTimeToSessionDuration(new TimeSpan(0, 5, 0));
        }
    }
}