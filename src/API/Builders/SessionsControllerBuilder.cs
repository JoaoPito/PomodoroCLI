using Pomogotchi.API.Controllers;
using Pomogotchi.Application.SoundPlayer;
using Pomogotchi.Application.Timer;
using Pomogotchi.Persistence;

namespace Pomogotchi.API.Builders
{
    public class SessionsControllerBuilder : IControllerBuilder
    {
        ISessionTimer _timer;
        IPlayer? _soundPlayer;
        IConfigLoader _config;
        
        public SessionsControllerBuilder()
        {
            AddConfigLoader();
            AddTimer();
            _soundPlayer = null;
        }

        void AddConfigLoader()
        {
            _config = ConfigLoader.GetController();
        }

        public void AddSoundPlayer()
        {
            _soundPlayer = new SFXPlayer(_config.GetSoundFilePath());
        }

        void AddTimer()
        {
            _timer = new SessionTimer(new SystemTimer());
        }

        public ApiControllerBase GetController()
        {
            var controller = new ApiController(_config, _timer, _soundPlayer);
            controller.LoadDefaultConfig();
            
            return controller;
        }
    }
}