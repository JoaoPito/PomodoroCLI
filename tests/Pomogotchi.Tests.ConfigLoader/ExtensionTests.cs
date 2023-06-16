using Pomogotchi.Tests.Mocks;
using Pomogotchi.API.Extensions;
using Pomogotchi.API.Extensions.Notifications;
using Pomogotchi.API.Controllers;

namespace Pomogotchi.Tests.ConfigLoader;

public class UnitTest1
{
    [Fact]
    public void TestIfIsAddingExtensionCorrectly()
    {
        // Create mock API controller
        var controller = new ApiControllerMock();
        // Add extension using extension method
        var extension = controller.AddConfigLoader();
        // Check controller extensions if contains configLoader
        Assert.Contains(extension, controller.Extensions);
    }

    [Fact]
    public void TestIfIsNotifyingConfigLoadToExtensions()
    {
        // Create mock API controller
        var controller = new ApiControllerMock();
        // Add notification history spy extension so that we can register received notifications
        var historyExtension = controller.AddNotificationHistory();
        // Add configLoader extension to controller
        var configExtension = controller.AddConfigLoader();
        // Call LoadConfig on extension
        configExtension.LoadConfig();
        // check mock's received notifications if contains ConfigLoadNotification
        bool receivedNotification = historyExtension.History.Any(reg => (reg.Item2.GetType() == typeof(ConfigLoadNotification)));
        Assert.True(receivedNotification, "Notification History has not received expected notification");
    }

    [Fact]
     public void TestReturnsFailedResultWhenCantLoadConfig()
    {
        // Create ConfigLoaderExtension with mock ConfigLoader
        var controller = new ApiControllerMock();
        var (extension, mock) = Helpers.CreateExtensionWithMock(controller);

        // Flag mock to fail next load
        mock.ThrowWhenLoadingConfig = new FileNotFoundException();

        // Call LoadConfig on extension
        var result = extension.LoadConfig();
        // Check if returns a failed Result
        Assert.False(result.Successful, "Config loader extension should have failed");
    }



    [Fact]
    public void TestIfIsSettingParamsCorrectly(){
        // Create ConfigLoaderExtension with mock ConfigLoader

        // Set parameter

        // Check if parameter exists on ConfigLoader.Parameters
        // Check if parameter is set on ConfigLoader.Parameters        
    }

    [Fact]
    public void TestIfIsGettingParamsCorrectly(){
        // Create ConfigLoaderExtension with mock ConfigLoader

        // Set parameter on ConfigLoader.Parameters

        // Check if parameter is set correctly using GetParam(key)
    }

    [Theory]
    [InlineData("")]
    [InlineData("./no_extension")]
    [InlineData("./wrong_extension.txt")]
    public void TestFailsWhenGivenInvalidFilePath(string path){
        // 
    }
}