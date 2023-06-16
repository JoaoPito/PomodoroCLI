using Pomogotchi.API.Controllers;
using Pomogotchi.Application.SoundPlayer;

namespace Pomogotchi.API.Extensions
{
    public static class SoundPlayerExtensionMethods
    {
        const string DEFAULT_SFX_FILE_PATH = "./sessionEnd.wav";
        public static SoundPlayerExtension AddSoundPlayerExtension(this ApiControllerBase controller){
            var extension = new SoundPlayerExtension(new VLCSoundPlayer());
            controller.AddExtension(extension);
            return extension;
        }

        public static SoundPlayerExtension AddSFXPlayerExtension(this ApiControllerBase controller){
            var soundPlayer = new VLCSoundPlayer();
            var extension = new SoundPlayerExtension(new SFXPlayer(soundPlayer, DEFAULT_SFX_FILE_PATH));
            controller.AddExtension(extension);
            return extension;
        }
    }
}