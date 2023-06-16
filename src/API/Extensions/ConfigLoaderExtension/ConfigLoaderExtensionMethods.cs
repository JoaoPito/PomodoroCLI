using Pomogotchi.API.Controllers;
using Pomogotchi.Application.ConfigLoader;

namespace Pomogotchi.API.Extensions
{
    public static class ConfigLoaderExtensionMethods
    {
        public static ConfigLoaderExtension AddConfigLoader(this ApiControllerBase controller)
        {
            var configLoader = ConfigLoader.GetController(null);
            var extension = new ConfigLoaderExtension(configLoader, controller);
            controller.AddExtension(extension);

            return extension;
        }
    }
}