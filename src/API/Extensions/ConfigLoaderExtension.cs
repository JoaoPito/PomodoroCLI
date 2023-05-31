using Pomogotchi.API.Extensions.Notifications;
using Pomogotchi.Domain;
using Pomogotchi.Persistence;

namespace Pomogotchi.API.Extensions
{
    public class ConfigLoaderExtension : IConfigExtension
    {
        private readonly ConfigLoader _loader;

        private Session _workParams;
        private Session _breakParams;
        public ConfigLoaderExtension(ConfigLoader loader)
        {
            this._loader = loader;
            _workParams = _loader.LoadWorkParams();
            _breakParams = _loader.LoadBreakParams();
        }  

        public void SetWorkParams(Session parameters)
        {
            _workParams = parameters;
        }

        public void SetBreakParams(Session parameters)
        {
            _breakParams = parameters;
        }

        public Session GetWorkParameters(){
            return _workParams;
        }
        public Session GetBreakParameters(){
            return _breakParams;
        }

        public void SaveAllChanges(){
            throw new NotImplementedException();
        }

        public IConfigLoader GetLoader(){
            return _loader;
        }

        public void Notify(GenericNotification notification)
        {
            
        }
    }
}