using NAudio.Wave;

namespace Pomogotchi.SoundPlayer
{
    public class SFXPlayer : IPlayer
    {
        string filePath;

        public SFXPlayer(string filePath)
        {
            this.filePath = filePath;
        }

        public void Play()
        {
            Thread thread = new Thread(playThread);
            thread.Start();
        }

        void playThread()
        {
            using (var audioFile = new AudioFileReader(filePath))
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();
                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(500);
                }
            }
        }

    }
}