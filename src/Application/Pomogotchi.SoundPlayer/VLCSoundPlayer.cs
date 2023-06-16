using FluentValidation;
using LibVLCSharp.Shared;

namespace Pomogotchi.Application.SoundPlayer
{
    public class VLCSoundPlayer : ISoundPlayer
    {
        LibVLC vlc;
        MediaPlayer player;

        public VLCSoundPlayer()
        {
            Core.Initialize();
            vlc = new LibVLC("--no-video");
            player = new MediaPlayer(vlc);
        }

        public void Play()
        {
            player.Play();
        }

        public void TogglePause(){
            player.Pause();
        }
        public void Stop()
        {
            player.Stop();
        }

        public void SetVolume(int percentage)
        {
            player.Volume = percentage;
        }

        public async void AttachMediaFile(string path)
        {
            var validator = new SoundFileHandler.FilePathValidator();
            validator.ValidateAndThrow(path);

            var media = new Media(vlc, path);
            await media.Parse(MediaParseOptions.ParseNetwork);

            if(media.SubItems.Count > 0)
                player.Media = media.SubItems.First();
            else
                player.Media = media;
        }
    }
}