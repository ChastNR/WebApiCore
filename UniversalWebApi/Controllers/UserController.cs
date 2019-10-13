using DataRepository.Contracts;
using DataRepository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using UniversalWebApi.Controllers.BaseControllers;

namespace UniversalWebApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseController<User>
    {
        private IUserRepository UserRepository => HttpContext.RequestServices.GetService<IUserRepository>();
    }
}