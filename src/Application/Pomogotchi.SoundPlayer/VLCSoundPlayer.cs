using LibVLCSharp.Shared;

namespace Pomogotchi.Application.SoundPlayer
{
    public class VLCSoundPlayer
    {
        LibVLC vlc;
        MediaPlayer player;

        public VLCSoundPlayer()
        {
            Core.Initialize();
            vlc = new LibVLC();
            player = new MediaPlayer(vlc);
        }

        public async void AttachMedia(string path){
            var media = new Media(vlc, path);
            await media.Parse(MediaParseOptions.ParseNetwork);

            if(media.SubItems.Count > 0)
                player.Media = media.SubItems.First();
            else
                player.Media = media;
        }

        public void Play()
        {
            player.Play();
        }

        public void TogglePause(){
            player.Pause();
        }
    }
}