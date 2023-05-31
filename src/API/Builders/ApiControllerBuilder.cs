using Pomogotchi.API.Controllers;
using Pomogotchi.API.Extensions;
using Pomogotchi.Application.Timer;
using Pomogotchi.Persistence;

namespace Pomogotchi.API.Builders
{
    public class ApiControllerBuilder : IControllerBuilder
    {
        ApiController controller;
        SessionExtensionController _session;
        ConfigLoaderExtension _config;
        
        public ApiControllerBuilder()
        {
            ResetExtensions();
        }

        void AddDefaultConfigLoader()
        {
            _config = new ConfigLoaderExtension(ConfigLoader.GetController());
            controller.AddExtension(_config);
        }

        void AddDefaultSessionController()
        {
            var sessionTimer = new SessionTimer(new SystemTimer());
            _session = new SessionExtensionController(controller, sessionTimer);
            controller.AddExtension(_session);
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

            AddDefaultConfigLoader();
            AddDefaultSessionController();
        }
    }
}