using Pomogotchi.API.Extensions.Notifications;
using Pomogotchi.Domain;
using Pomogotchi.Application.ConfigLoader;

namespace Pomogotchi.API.Extensions
{
    public class ConfigLoaderExtension : IConfigExtension
    {
        private readonly ConfigLoader _loader;
        public ConfigLoaderExtension(ConfigLoader loader)
        {
            this._loader = loader;
        }  

        public void SetWorkParams(Session parameters)
        {
            _loader.SetWorkParams(parameters);
        }

        public void SetBreakParams(Session parameters)
        {
           _loader.SetBreakParams(parameters);
        }

        public Session GetWorkParameters(){
            return _loader.GetWorkParams();
        }
        public Session GetBreakParameters(){
            return _loader.GetBreakParams();
        }

        public void SaveAllChanges(){
            _loader.SaveChanges();
        }

        public IConfigLoader GetLoader(){
            return _loader;
        }

        public void SetExtensionParam<T>(string key, T data){
            _loader.SetExtensionParam<T>(key,data);
        }
        public T GetExtensionParam<T>(string key){
            return _loader.GetExtensionParam<T>(key);
        }

        public void Notify(GenericNotification notification)
        {
            if(notification.GetType() == typeof(ConfigSaveNotification))
                SaveAllChanges();
        }
    }
}