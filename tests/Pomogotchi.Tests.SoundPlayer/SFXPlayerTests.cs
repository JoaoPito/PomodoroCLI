
using Pomogotchi.Application.SoundPlayer;
using Pomogotchi.Tests.Mocks;

namespace Pomogotchi.Tests.SoundPlayer
{
    public class SFXPlayerTests
    {
        const int EXPECTED_VOLUME = 100;

        [Fact]
        public void TestIfBuildsSoundPlayerCorrectly(){
            // Create a new SFXPlayer, attaching a mock SoundPlayer
            var mock = new MockSoundPlayer();
            var sfxPlayer = new SFXPlayer(mock, Helpers.VALID_FILE_PATH);
            // on the mock, after the SFXPlayer constructor is called, 
            // check if the media is attached 
            Assert.Equal(Helpers.VALID_FILE_PATH, mock.MediaFilePath);
            //and the volume is set
            Assert.Equal(EXPECTED_VOLUME, mock.Volume);
        }

        [Fact]
        public void TestIfPlaysSoundCorrectly(){
            // Create a new SFXPlayer, attaching a mock SoundPlayer
            var mock = new MockSoundPlayer();
            var sfxPlayer = new SFXPlayer(mock, Helpers.VALID_FILE_PATH);
            // Call Play()
            sfxPlayer.Play();
            // Check if mock.hasPlayed has been set
            Assert.True(mock.HasPlayed, "SFXPlayer did not call Play on the SoundPlayer");
        }
    }
}