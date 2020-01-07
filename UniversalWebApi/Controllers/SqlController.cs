using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using DataAccess.Interfaces.Base;

namespace UniversalWebApi.Controllers
{
    public abstract class SqlController<T> : ApiBaseController<SqlController<T>, ISqlRepository> where T : class
    {
        protected SqlController(ILogger<SqlController<T>> logger, ISqlRepository service) : base(logger, service)
        {
        }

        [HttpGet]
        public Task<IEnumerable<T>> Get()
        {
            return _service.GetAllAsync<T>();
        }

        [HttpGet("{id:int}")]
        public Task<T> Get(int id)
        {
            return _service.GetAsync<T>(id);
        }

        [HttpPost]
        public Task Post([FromBody] T t)
        {
            return _service.InsertAsync(t);
        }

        [HttpPut]
        public Task Put([FromBody] T t)
        {
            return _service.UpdateAsync(t);
        }

        [HttpDelete("{id:int}")]
        public Task Delete(int id)
        {
            return _service.DeleteRowAsync<T>(id);
        }
    }
}