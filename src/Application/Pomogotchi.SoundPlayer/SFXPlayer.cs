namespace Pomogotchi.Application.SoundPlayer
{
    public class SFXPlayer : IPlayer
    {
        string _filePath;
        VLCSoundPlayer _vlcPlayer = new();

        public SFXPlayer(string filePath)
        {
            this._filePath = filePath;
            _vlcPlayer.AttachMedia(filePath);
        }

        public void Play()
        {
            _vlcPlayer.Play();
        }
    }
}