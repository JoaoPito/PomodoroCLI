using API;
using Pomogotchi.API.Extensions;
using Pomogotchi.API.Extensions.Notifications;
using static Pomogotchi.API.Extensions.IAPIExtension;

namespace Pomogotchi.API.Controllers
{
    public abstract class ApiControllerBase : IApiComponent
    {
        //protected IMediator _mediator;
        //public IMediator Mediator => _mediator;

        protected List<IAPIExtension> _extensions = new();

        public void AddExtension(IAPIExtension extension){
            _extensions.Add(extension);
        }

        public void RemoveExtension(IAPIExtension extension){
            _extensions.Remove(extension);
        }

        public IAPIExtension GetExtension(Type extensionType){
            try
            {
                var extension = _extensions.Where((extension) => extension.GetType() == extensionType).First();                
                return extension;
            }
            catch (System.ArgumentNullException)
            {
                throw new ExtensionNotFoundException();
            }
        }

        public void Notify(GenericNotification notification){
            _extensions.ForEach((extension) => extension.Notify(notification));
        }
    }
}