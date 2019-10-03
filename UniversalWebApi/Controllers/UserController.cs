using UniversalWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using SqlRepository.Interfaces;
using UniversalWebApi.Controllers.BaseControllers;
using UniversalWebApi.Helpers.Serializer;
using UniversalWebApi.Helpers.ExceptionManager;

namespace UniversalWebApi.Controllers
{
    //[ServiceFilter(typeof(ApiAsyncActionFilter))]
    [Route("api/[controller]")]
    public class UserController : BaseController<User>
    {
     
    }
}