using Pomogotchi.API.Controllers;
using Pomogotchi.API.Extensions;

namespace Pomogotchi.Tests.Mocks
{
    public class ApiControllerMock : ApiControllerBase
    {
        public List<IAPIExtension> Extensions => base._extensions;

        public ApiControllerMock()
        {
        }
    
    }
}