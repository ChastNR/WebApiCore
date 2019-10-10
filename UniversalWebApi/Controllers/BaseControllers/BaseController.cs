using System.Collections.Generic;
using System.Threading.Tasks;
using DataRepository.Interfaces.Base;
using Microsoft.AspNetCore.Mvc;
using UniversalWebApi.Filters;

namespace UniversalWebApi.Controllers.BaseControllers
{
    [ServiceFilter(typeof(ApiExceptionFilter))]
    public abstract class BaseController<T> : Controller where T : class
    {
        private ISqlRepository Db => (ISqlRepository)HttpContext.RequestServices.GetService(typeof(ISqlRepository));
        
        [HttpGet]
        public async Task<IEnumerable<T>> Get() => await Db.GetAllAsync<T>();

        [HttpGet("{id:int}")]
        public async Task<T> Get(int id) => await Db.GetAsync<T>(id);

        [HttpPost]
        public async Task Post([FromBody] T entity) => await Db.InsertAsync(entity);

        [HttpPut]
        public async Task Put([FromBody] T entity) => await Db.UpdateAsync(entity);

        [HttpDelete("{id:int}")]
        public async Task Delete(int id) => await Db.DeleteRowAsync<T>(id);
    }
}