using Pomogotchi.API.Controllers;
using Pomogotchi.API.Extensions;

namespace Pomogotchi.API.Builders
{
    public interface IControllerBuilder
    {
        public void AddExtension(IAPIExtension extension);
        public ApiControllerBase GetController();
    }
}