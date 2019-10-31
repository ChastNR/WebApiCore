using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataRepository.Contracts;
using DataRepository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using UniversalWebApi.Filters;

namespace UniversalWebApi.Controllers
{
    //[ServiceFilter(typeof(ApiExceptionFilter))]
    //[Authorize]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IUserRepository UserDb => HttpContext.RequestServices.GetRequiredService<IUserRepository>();

        [HttpGet]
        public Task<IEnumerable<User>> Get() => UserDb.GetAllAsync<User>();

        [HttpGet("{id:Guid}")]
        public Task<User> Get(Guid id) => UserDb.GetAsync<User>(id);

        [HttpPost]
        public Task Post([FromBody] User user) => UserDb.InsertAsync(user);

        [HttpPost("ReturnId")]
        public Task<Guid> InsertReturnId([FromBody] User user) => UserDb.InsertUserAsyncWithReturnId(user);

        [HttpPut]
        public Task Put([FromBody] User user) => UserDb.UpdateAsync(user);

        [HttpDelete("{id:Guid}")]
        public Task Delete(Guid id) => UserDb.DeleteRowAsync<User>(id);
    }
}