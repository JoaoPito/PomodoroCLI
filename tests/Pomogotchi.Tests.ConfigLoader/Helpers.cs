using Pomogotchi.API.Controllers;
using Pomogotchi.API.Extensions;

namespace Pomogotchi.Tests.ConfigLoader
{
    public static class Helpers
    {
        public static (ConfigLoaderExtension, ConfigLoaderMock) CreateExtensionWithMock(ApiControllerBase controller)
        {
            var mock = new ConfigLoaderMock();
            return (new ConfigLoaderExtension(mock, controller), mock);
        }
    }
}