using UniversalWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using SqlRepository.Interfaces;
using UniversalWebApi.Helpers.ExceptionManager;
using UniversalWebApi.Helpers.Filters;

namespace UniversalWebApi.Controllers
{
    [ServiceFilter(typeof(ApiAsyncActionFilter))]
    [Route("api/[controller]")]
    public class UserController : BaseController<User>
    {
        public UserController(IDataRepository db, IExceptionManager manager) : base(db, manager)
        {
        }
    }
}