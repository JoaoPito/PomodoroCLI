using Pomogotchi.Application.SoundPlayer;

namespace Pomogotchi.Tests.Mocks
{
    public class MockSoundPlayer : ISoundPlayer
    {
        public MockSoundPlayer()
        {

        }

        public string MediaFilePath { get; protected set; }
        public bool HasPlayed { get; protected set; }
        public int Volume { get; protected set; }

        public void AttachMediaFile(string path)
        {
            MediaFilePath = path;
        }

        public void Play()
        {
            HasPlayed = true;
        }

        public void SetVolume(int percentage)
        {
            Volume = percentage;
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void TogglePause()
        {
            throw new NotImplementedException();
        }
    }
}