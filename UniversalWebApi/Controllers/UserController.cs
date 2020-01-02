using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using DataRepository.Contracts;
using DataRepository.Interfaces;

using UniversalWebApi.Controllers.BaseControllers;

namespace UniversalWebApi.Controllers
{
    public class UserController : BaseController<UserController, IUserRepository>
    {
        public UserController(ILogger<UserController> logger, IUserRepository service) : base(logger, service)
        {
        }

        [HttpGet]
        public Task<IEnumerable<User>> Get() => _service.GetAllAsync<User>();

        [HttpGet("{id:Guid}")]
        public Task<User> Get(Guid id) => _service.GetAsync<User>(id);

        [HttpPost]
        public Task Post([FromBody] User user) => _service.InsertAsync(user);

        [HttpPost("ReturnId")]
        public Task<Guid> InsertReturnId([FromBody] User user) => _service.InsertUserAsyncWithReturnId(user);

        [HttpPut]
        public Task Put([FromBody] User user) => _service.UpdateAsync(user);

        [HttpDelete("{id:Guid}")]
        public Task Delete(Guid id) => _service.DeleteRowAsync<User>(id);
    }
}