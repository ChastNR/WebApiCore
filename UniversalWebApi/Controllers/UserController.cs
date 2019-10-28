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
    [ServiceFilter(typeof(ApiExceptionFilter))]
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IUserRepository UserDb => HttpContext.RequestServices.GetRequiredService<IUserRepository>();

        [HttpGet]
        public async Task<IEnumerable<User>> Get() => await UserDb.GetAllAsync<User>();

        [HttpGet("{id:Guid}")]
        public async Task<User> Get(Guid id) => await UserDb.GetAsync<User>(id);

        [HttpPost]
        public async Task Post([FromBody] User user) => await UserDb.InsertAsync(user);

        [HttpPost("ReturnId")]
        public async Task<Guid> InsertReturnId([FromBody] User user) => await UserDb.InsertUserAsyncWithReturnId(user);

        [HttpPut]
        public async Task Put([FromBody] User user) => await UserDb.UpdateAsync(user);

        [HttpDelete("{id:Guid}")]
        public async Task Delete(Guid id) => await UserDb.DeleteRowAsync<User>(id);
    }
}