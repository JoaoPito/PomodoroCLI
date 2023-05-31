using System;
using Pomogotchi.API.Builders;
using Pomogotchi.Domain;
using Pomogotchi.API.Extensions;
using Pomogotchi.Application.SoundPlayer;
using Pomogotchi.API.Controllers;
using Pomogotchi.API.Extensions.SessionExtension;

namespace PomoGUI.Models
{
    public class MainWindowController
    {
        ApiControllerBase _controller;
        public ApiControllerBase Controller => _controller;

        SessionExtensionController _sessionController;
        public SessionExtensionController SessionController => _sessionController;

        ConfigLoaderExtension _configController;

        public MainWindowController(Action? onSessionEnd, Action? onTimerUpdate)
        {
            BuildAPI();

            _sessionController = (SessionExtensionController)_controller.GetExtension(typeof(SessionExtensionController));
            _sessionController.EndTriggers += onSessionEnd;

            _configController = (ConfigLoaderExtension)(_controller.GetExtension(typeof(ConfigLoaderExtension)));

            if (onTimerUpdate != null) _sessionController.Timer.RegisterUpdateTrigger(onTimerUpdate);
        }

        void BuildAPI()
        {
            var builder = new ApiControllerBuilder();
            _controller = builder.GetController();

            //var configLoader = (ConfigLoaderExtension)(_controller.GetExtension(typeof(ConfigLoaderExtension)));
           // var soundPlayer = new SFXPlayer(configLoader.GetLoader().GetSoundFilePath());
            //_controller.AddExtension(new SoundPlayerExtension(soundPlayer));
        }

        bool ValidateTimerDuration(TimeSpan amount)
        {
            var sessionParameters = _sessionController.Session.Parameters;
            return (sessionParameters.Duration + amount).TotalMinutes >= 5 && (sessionParameters.Duration + amount).TotalHours < 24;
        }

        public void AddTimeToSessionDuration(TimeSpan amount)
        {
            if (ValidateTimerDuration(amount))
            {
                var currentSession = _sessionController.Session;
                var newSessionParams = new Session(currentSession.Parameters.Duration + amount, currentSession.Parameters.Type);

                SetSessionParams(_configController, currentSession, newSessionParams);
                _sessionController.Session.LoadConfig(_configController);
            }
        }

        void SetSessionParams(ConfigLoaderExtension configLoader, SessionType session, Session parameters){
            if(session.Parameters.Type == Session.SessionType.Work) configLoader.SetWorkParams(parameters);
            else if(session.Parameters.Type == Session.SessionType.Break) configLoader.SetBreakParams(parameters);
        }
        public void StopCurrentSession()
        {
            _sessionController.Stop();
        }

        public void LoadNextSession()
        {
            var nextSession = _sessionController.Session.GetNextSession();
            nextSession.LoadConfig(_configController);
            _sessionController.SwitchSessionTo(nextSession);

        }
    }
}