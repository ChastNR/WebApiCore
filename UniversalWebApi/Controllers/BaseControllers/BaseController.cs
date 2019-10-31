using System.Collections.Generic;
using System.Threading.Tasks;
using DataRepository.Interfaces.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using UniversalWebApi.Filters;

namespace UniversalWebApi.Controllers.BaseControllers
{
    [ServiceFilter(typeof(ApiExceptionFilter))]
    [Authorize]
    public abstract class BaseController<T> : Controller where T : class
    {
        private ISqlRepository Db => HttpContext.RequestServices.GetRequiredService<ISqlRepository>();

        [HttpGet]
        public Task<IEnumerable<T>> Get() => Db.GetAllAsync<T>();

        [HttpGet("{id:int}")]
        public Task<T> Get(int id) => Db.GetAsync<T>(id);

        [HttpPost]
        public Task Post([FromBody] T entity) => Db.InsertAsync(entity);

        [HttpPut]
        public Task Put([FromBody] T entity) => Db.UpdateAsync(entity);

        [HttpDelete("{id:int}")]
        public Task Delete(int id) => Db.DeleteRowAsync<T>(id);
    }
}