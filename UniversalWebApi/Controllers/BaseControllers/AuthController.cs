using System.Threading.Tasks;
using AuthenticationProcessor.Contracts;
using AuthenticationProcessor.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace UniversalWebApi.Controllers.BaseControllers
{
    //[ServiceFilter(typeof(ApiExceptionFilter))]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private IAuthProcessor Processor => HttpContext.RequestServices.GetService<IAuthProcessor>();

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegistrationContract contract)
        {
            var result = await Processor.Register(contract);
            return result ? (IActionResult) Ok() : BadRequest();
        }
    }
}