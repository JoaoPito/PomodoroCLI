namespace Pomogotchi.Tests.SoundPlayer;

using Pomogotchi.API.Extensions;
using Pomogotchi.API.Extensions.Notifications;
using Pomogotchi.Tests.Mocks;

public class ExtensionTests
{
    [Fact]
    public void TestIfSoundPlayerExtensionIsAddedCorrectly()
    {
        // Adiciona extensão ao mock usando extension method
        var controllerMock = new ApiControllerMock();
        var extension = controllerMock.AddSoundPlayerExtension();
        // Ver se extensão foi adicionada à lista no mock
        Assert.Contains(extension, controllerMock.Extensions);
    }

    [Fact]
    public void TestIfSFXPlayerExtensionIsAddedCorrectly()
    {
        // Adiciona extensão ao mock usando extension method
        var (controllerMock, extension) = Helpers.CreateControllerMockAndAddSFXExtension();
        // Ver se extensão foi adicionada à lista no mock
        Assert.Contains(extension, controllerMock.Extensions);
    }

    [Fact]
    public void TestIfLoadsConfigCorrectly()
    {
        // Create extension with SoundPlayer mock
        var (extension, soundPlayerMock) = Helpers.CreateExtensionWithMock();

        // Create ConfigParams and adds it to ConfigLoader extension mock
        var configLoaderMock = Helpers.CreateConfigLoaderMockWithFilePath(Helpers.VALID_FILE_PATH);

        // Emits ConfigLoad notification for extension
        var notification = new ConfigLoadNotification(configLoaderMock);
        var notifyResult = extension.Notify(notification);
        
        // Checks if notification result is successful
        Assert.True(notifyResult.Successful, "Sound Player could not load config successfully");

        // Checks if file path name is loaded correctly
        Assert.Equal(Helpers.VALID_FILE_PATH, soundPlayerMock.MediaFilePath);
    }

    [Theory]
    [InlineData("./notFoundFile.mp3", Helpers.FILE_NOT_FOUND_ERROR_MESSAGE)]
    [InlineData("./invalidFileFormat", Helpers.INVALID_FILE_ERROR_MESSAGE)]
    [InlineData("./invalidExtension.txt", Helpers.UNSUPPORTED_FILE_EXTENSION_ERROR_MESSAGE)]
    [InlineData("", Helpers.INVALID_FILE_ERROR_MESSAGE)]
    [InlineData(null, Helpers.NULL_ARGUMENT_ERROR_MESSAGE)]
    public void TestIfFailsWhenGivenInvalidSoundFile(string filePath, string expectedMsg)
    {
        // Adds extension to mock
        var (controller, extension) = Helpers.CreateControllerMockAndAddSFXExtension();

        // Create ConfigParams with given path
        var configLoader = Helpers.CreateConfigLoaderMockWithFilePath(filePath);

        // Sends ConfigLoad notification to extension
        var notification = new ConfigLoadNotification(configLoader);
        var result = extension.Notify(notification);

        // Checks if notification result failed
        Assert.False(result.Successful, "Config load notification should have failed");
        Assert.Contains(expectedMsg, result.Error);
    }

    

}