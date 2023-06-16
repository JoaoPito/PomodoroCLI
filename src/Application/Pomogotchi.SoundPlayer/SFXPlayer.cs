namespace Pomogotchi.Application.SoundPlayer
{
    public class SFXPlayer : IPlayer
    {
        string _filePath;
        ISoundPlayer _player;

        public SFXPlayer(ISoundPlayer player, string filePath)
        {
            _player = player;
            AttachMediaFile(filePath);
            _player.SetVolume(100);
        }

        public void AttachMediaFile(string path)
        {
            SoundFileHandler.ValidateSoundFilePath(path);

            this._filePath = path;
            _player.AttachMediaFile(path);
        }

        public void Play()
        {
            _player.Play();
        }
    }
}