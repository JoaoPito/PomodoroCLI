using Pomogotchi.API.Controllers;
using System;
using Pomogotchi.API.Builders;

namespace PomoGUI.Models
{
    public class MainWindowController
    {
        ControllerBase _controller;
        public ControllerBase Controller => _controller;

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
    }
}