using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using DataAccess.Contracts;
using DataAccess.Interfaces;

namespace UniversalWebApi.Controllers
{
    public class UserController : ApiBaseController<UserController, IUserRepository>
    {
        public UserController(ILogger<UserController> logger, IUserRepository service) : base(logger, service)
        {
        }

        [HttpGet]
        public Task<IEnumerable<User>> Get()
        {
            return _service.GetAllAsync<User>();
        }

        [HttpGet("{id:Guid}")]
        public Task<User> Get(Guid id)
        {
            return _service.GetAsync<User>(id);
        }

        [HttpPost]
        public Task Post([FromBody] User user)
        {
            return _service.InsertAsync(user);
        }

        [HttpPost("ReturnId")]
        public Task<Guid> InsertReturnId([FromBody] User user)
        {
            return _service.InsertUserWithReturnIdAsync(user);
        }

        [HttpPut]
        public Task Put([FromBody] User user)
        {
            return _service.UpdateAsync(user);
        }

        [HttpDelete("{id:Guid}")]
        public Task Delete(Guid id)
        {
            return _service.DeleteRowAsync<User>(id);
        }
    }
}