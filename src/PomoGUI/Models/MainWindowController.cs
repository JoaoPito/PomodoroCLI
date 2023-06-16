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
        ConfigLoaderExtension _configController;

        public bool InSession { get => _sessionController.InSession; }
        public SessionType CurrentSession { get => _sessionController.Session; } 
        public TimeSpan RemainingTime { get => _sessionController.Timer.GetRemainingTime(); }
        public TimeSpan TotalDuration { get => _sessionController.Duration; }

        public MainWindowController(Action? onSessionEnd, Action? onTimerUpdate)
        {
            BuildAPI();

            _sessionController = (SessionExtensionController)_controller.GetExtension(typeof(SessionExtensionController));
            _sessionController.EndTriggers += onSessionEnd;

            _configController = (ConfigLoaderExtension)_controller.GetExtension(typeof(ConfigLoaderExtension));

            if (onTimerUpdate != null) _sessionController.Timer.RegisterUpdateTrigger(onTimerUpdate);

            SaveConfig();
        }

        void BuildAPI()
        {
            var builder = new ApiControllerBuilder();
            _controller = builder.GetController();
            _controller.AddSFXPlayerExtension();
        }

        string TryToLoadSoundPlayerConfig(ConfigLoaderExtension configLoader){
            try
            {
                return configLoader.GetExtensionParam("soundPlayer_filePath");
            }
            catch (System.ArgumentException)
            {
                var defaultPath =  "./sessionEnd.wav";
                configLoader.SetExtensionParam("soundPlayer_filePath",defaultPath);
                return defaultPath;
            }
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
                var newSessionParams = new Session(currentSession.Duration + amount);

                SetSessionParams(_configController, currentSession, newSessionParams);
                _sessionController.Session.LoadConfig(_configController);
            }
        }

        public void SaveConfig(){
            _configController.SaveAllChanges();
        }

        void SetSessionParams(ConfigLoaderExtension configLoader, SessionType session, Session parameters)
        {
            if (session.GetType() == typeof(WorkSession)) configLoader.SetWorkParams(parameters);
            else if (session.GetType() == typeof(BreakSession)) configLoader.SetBreakParams(parameters);
        }

        public void StartSession(){
            _sessionController.Start();
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