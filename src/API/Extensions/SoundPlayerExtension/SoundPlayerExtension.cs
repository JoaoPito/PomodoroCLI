using Pomogotchi.API.Controllers;
using Pomogotchi.API.Extensions.Notifications;
using Pomogotchi.Application.SoundPlayer;
using Pomogotchi.Domain;

namespace Pomogotchi.API.Extensions
{
    public class SoundPlayerExtension : IAPIExtension
    {
        const string FILE_PATH_CONFIG_KEY = "soundPlayer_filePath";
        IPlayer _player;
        public SoundPlayerExtension(IPlayer player)
        {
            _player = player;
        }
        public Result Notify(GenericNotification notification)
        {
            if(notification.GetType() == typeof(SessionEndNotification))
                return TryPlaySessionEndSound();

            if(notification.GetType() == typeof(ConfigLoadNotification))
                return HandleConfigLoadNotification((ConfigLoadNotification) notification);
            
            return Result.Success();
        }

        Result TryPlaySessionEndSound()
        {
            try
            {
                _player?.Play();
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        Result HandleConfigLoadNotification(ConfigLoadNotification notification){
            try
            {
                var config = (IConfigExtension) notification.Context;
                LoadConfig(config);
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }

            return Result.Success();
        }

        void LoadConfig(IConfigExtension configExtension){
            var filePath = configExtension.GetExtensionParam(FILE_PATH_CONFIG_KEY);
            _player.AttachMediaFile(filePath);
        }
    }
}