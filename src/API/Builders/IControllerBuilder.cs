using Pomogotchi.API.Controllers;
using Pomogotchi.API.Builders.Commands;

namespace Pomogotchi.API.Builders
{
    public interface IControllerBuilder
    {
        //public void AddExtension(IExtensionCommand feature);
        public void AddSoundPlayer();
        public ApiControllerBase GetController();
    }
}