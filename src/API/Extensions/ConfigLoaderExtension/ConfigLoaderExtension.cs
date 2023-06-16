using Pomogotchi.API.Extensions.Notifications;
using Pomogotchi.Application.ConfigLoader;
using Pomogotchi.API.Controllers;

namespace Pomogotchi.API.Extensions
{
    public class ConfigLoaderExtension : IConfigExtension
    {
        private readonly IConfigLoader _loader;
        private readonly ApiControllerBase _controller;
        public ConfigLoaderExtension(IConfigLoader loader, ApiControllerBase controller)
        {
            this._controller = controller;
            this._loader = loader;
        }

        public void SaveAllChanges()
        {
            _controller.NotifyAllExtensions(new ConfigSaveNotification(this));
            _loader.SaveChanges();
        }

        public CommandResult Notify(GenericNotification notification)
        {
            return CommandResult.Success();
        }

        public CommandResult ReloadAllConfigs()
        {
            var result = HandleConfigReload();
            _controller.NotifyAllExtensions(new ConfigLoadNotification(this));
            return result;
        }

        CommandResult HandleConfigReload()
        {
            try
            {
                _loader.ReloadConfig();
            }
            catch (FileNotFoundException)
            {
                LoadAndCreateDefaults();
                return CommandResult.Failure("File not found. Defaults loaded");
            }
            catch (Exception ex)
            {
                return CommandResult.Failure(ex.Message);
            }

            return CommandResult.Success();
        }

        void LoadAndCreateDefaults()
        {
            _loader.LoadDefaults();
            _loader.SaveChanges();
        }

        public T? GetParamAs<T>(string key)
        {
            return _loader.GetParamAs<T>(key);
        }

        public void SetParamAs<T>(string key, T data)
        {
            _loader.SetParamAs<T>(key, data);
        }
    }
}