using Pomogotchi.Tests.Mocks;
using Pomogotchi.API.Extensions;
using Pomogotchi.API.Extensions.Notifications;

namespace Pomogotchi.Tests.ConfigLoader;

public class ExtensionTests
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
        // Reload config on extension
        configExtension.ReloadAllConfigs();
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

        // Reload config
        var result = extension.ReloadAllConfigs();
        // Check if returns a failed Result
        Assert.False(result.Successful, "Config loader extension should have failed");
    }



    [Fact]
    public void TestIfIsSettingParamsCorrectly()
    {
        string paramKey = "example_key";
        string paramValue = "example";

        // Create ConfigLoaderExtension with mock ConfigLoader
        var controller = new ApiControllerMock();
        var (extension, mock) = Helpers.CreateExtensionWithMock(controller);

        // Set parameter
        extension.SetParamAs<string>(paramKey, paramValue);

        // Check if parameter exists on ConfigLoader.Parameters
        var paramCreated = mock.Parameters.ContainsKey(paramKey);
        Assert.True(paramCreated, "Parameter key has not been created");
        // Check if parameter is set correctly on ConfigLoader.Parameters
        if (paramCreated)
        {
            var result = mock.Parameters[paramKey];
            Assert.Contains(paramValue, result);
        }

    }

    [Fact]
    public void TestIfIsGettingParamsCorrectly()
    {
        string paramKey = "example_key";
        string paramValue = "example";

        // Create ConfigLoaderExtension with mock ConfigLoader
        var controller = new ApiControllerMock();
        var (extension, mock) = Helpers.CreateExtensionWithMock(controller);

        // Set parameter on ConfigLoader.Parameters
        mock.SetParamAs<string>(paramKey, paramValue);

        // Check if parameter is set correctly using GetParam(key)
        var result = extension.GetParamAs<string>(paramKey);
        Assert.Equal(paramValue, result);
    }

    [Fact]
    public void TestIfCanGetExtensionFromController()
    {
        var controller = new ApiControllerMock();
        var extension = controller.AddConfigLoader();

        Assert.Equal(extension, controller.GetConfigLoader());
    }
}