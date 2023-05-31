using API;
using Pomogotchi.API.Extensions;
using Pomogotchi.API.Extensions.Notifications;

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

            var extension = _extensions.Where((extension) => extension.GetType() == extensionType).First();
            
            if(extension == null)
                throw new IAPIExtension.ExtensionNotFoundException();

            return extension;
        }

        public void Notify(GenericNotification notification){
            _extensions.ForEach((extension) => extension.Notify(notification));
        }
    }
}