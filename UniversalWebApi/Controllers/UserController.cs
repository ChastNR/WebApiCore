using UniversalWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using UniversalWebApi.Controllers.BaseControllers;

namespace UniversalWebApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseController<User>
    {
    }
}