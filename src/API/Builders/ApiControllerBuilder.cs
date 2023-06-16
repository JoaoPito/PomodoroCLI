using Pomogotchi.API.Controllers;
using Pomogotchi.API.Extensions;
using Pomogotchi.Application.Timer;

namespace Pomogotchi.API.Builders
{
    public class ApiControllerBuilder : IControllerBuilder
    {
        ApiController controller = new();
        
        public ApiControllerBuilder()
        {
            ResetExtensions();
        }

        void AddDefaultSessionController()
        {
            var sessionTimer = new SessionTimer(new SystemTimer());
            var extension = new SessionExtensionController(controller, sessionTimer);
            controller.AddExtension(extension);
        }

        public ApiControllerBase GetController()
        {            
            return controller;
        }

        public void AddExtension(IAPIExtension extension)
        {
            throw new NotImplementedException();
        }

        public void ResetExtensions(){
            controller = new ApiController();

            controller.AddConfigLoader();
            //controller.AddSessionTimer();
            AddDefaultSessionController();
        }
    }
}