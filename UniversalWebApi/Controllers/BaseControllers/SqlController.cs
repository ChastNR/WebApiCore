using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using DataRepository.Interfaces.Base;

namespace UniversalWebApi.Controllers.BaseControllers
{
    public abstract class SqlController<T> : BaseController<SqlController<T>, ISqlRepository> where T : class
    {
        protected SqlController(ILogger<SqlController<T>> logger, ISqlRepository service) : base(logger, service)
        {
        }

        [HttpGet]
        public Task<IEnumerable<T>> Get() => _service.GetAllAsync<T>();

        [HttpGet("{id:int}")]
        public Task<T> Get(int id) => _service.GetAsync<T>(id);

        [HttpPost]
        public Task Post([FromBody] T t) => _service.InsertAsync(t);

        [HttpPut]
        public Task Put([FromBody] T t) => _service.UpdateAsync(t);

        [HttpDelete("{id:int}")]
        public Task Delete(int id) => _service.DeleteRowAsync<T>(id);
    }
}