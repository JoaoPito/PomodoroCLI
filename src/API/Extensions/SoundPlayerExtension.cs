using Pomogotchi.API.Extensions;
using Pomogotchi.API.Extensions.Notifications;
using Pomogotchi.Application.SoundPlayer;

namespace Pomogotchi.API.Extensions
{
    public class SoundPlayerExtension : IAPIExtension
    {
        IPlayer _player;
        public SoundPlayerExtension(IPlayer player)
        {
            _player = player;
        }

        public void Notify(GenericNotification notification)
        {
            if(notification.GetType() == typeof(SessionEndNotification))
                TryPlaySessionEndSound();
        }

        bool TryPlaySessionEndSound()
        {
            try
            {
                _player?.Play();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


    }
}