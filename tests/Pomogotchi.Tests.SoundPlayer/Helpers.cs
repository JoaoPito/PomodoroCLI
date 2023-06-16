using Pomogotchi.Tests.Mocks;
using Pomogotchi.API.Extensions;
using Pomogotchi.Domain;

namespace Pomogotchi.Tests.SoundPlayer
{
    public static class Helpers
    {
        public const string VALID_FILE_PATH = "./sessionEnd.wav";
        public const string CONFIG_PARAM_KEY = "soundPlayer_filePath";

        public const string INVALID_FILE_ERROR_MESSAGE = "Invalid media file path";
        public const string FILE_NOT_FOUND_ERROR_MESSAGE = "Could not find file: ";
        public const string UNSUPPORTED_FILE_EXTENSION_ERROR_MESSAGE = "Cannot open file with extension: ";
        public const string NULL_ARGUMENT_ERROR_MESSAGE = "Cannot pass null model to Validate.";

        public static Dictionary<string,string> CreateEmptyConfigParams()
        {
            return new Dictionary<string,string>();
        }

        public static (ApiControllerMock, SoundPlayerExtension) CreateControllerMockAndAddSFXExtension()
        {
            var controllerMock = new ApiControllerMock();
            return (controllerMock, controllerMock.AddSFXPlayerExtension());
        }

        public static (SoundPlayerExtension, MockSoundPlayer) CreateExtensionWithMock()
        {
            var player = new MockSoundPlayer();
            return (new SoundPlayerExtension(player), player);
        }

        public static ConfigLoaderExtensionMock CreateConfigLoaderMockWithFilePath(string filePath)
        {
            var config = Helpers.CreateEmptyConfigParams();
            config.Add(Helpers.CONFIG_PARAM_KEY, filePath);
            return new ConfigLoaderExtensionMock(config);
        }
    }
}