using FluentValidation;
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
        public CommandResult Notify(GenericNotification notification)
        {
            if(notification.GetType() == typeof(SessionEndNotification))
                return TryPlaySessionEndSound();

            if(notification.GetType() == typeof(ConfigLoadNotification))
                return HandleConfigLoadNotification((ConfigLoadNotification) notification);
            
            return CommandResult.Success();
        }

        CommandResult TryPlaySessionEndSound()
        {
            try
            {
                _player?.Play();
                return CommandResult.Success();
            }
            catch (Exception ex)
            {
                return CommandResult.Failure(ex.Message);
            }
        }

        CommandResult HandleConfigLoadNotification(ConfigLoadNotification notification){
            try
            {
                var config = (IConfigExtension) notification.Context;
                LoadConfig(config);
            }
            catch (ValidationException ex)
            {
                return CommandResult.Failure(ex.Message);
            }
            catch(ArgumentNullException ex)
            {
                return CommandResult.Failure(ex.Message);
            }

            return CommandResult.Success();
        }

        void LoadConfig(IConfigExtension configExtension){
            var filePath = configExtension.GetParam(FILE_PATH_CONFIG_KEY);
            _player.AttachMediaFile(filePath);
        }
    }
}