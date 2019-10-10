using System.Collections.Generic;
using System.Threading.Tasks;
using DataRepository.Interfaces.Base;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using UniversalWebApi.Filters;

namespace UniversalWebApi.Controllers.BaseControllers
{
    [ServiceFilter(typeof(ApiExceptionFilter))]
    public abstract class MongoDbController<T> : Controller where T : class, IMongoDoc
    {
        protected IMongoRepository Db => (IMongoRepository)HttpContext.RequestServices.GetService(typeof(IMongoRepository));

        [HttpGet]
        public async Task<IEnumerable<T>> Get() => await Db.GetAsync<T>();

        [HttpGet("{id}")]
        public async Task<T> Get(ObjectId id) => await Db.GetAsync<T>(id);

        [HttpPost]
        public  async Task Post([FromBody] T entity) => await Db.AddAsync(entity);

        [HttpPut]
        public  async Task Put([FromBody] T entity) => await Db.UpdateAsync(entity);

        [HttpDelete("{id}")]
        public  async Task Delete(ObjectId id) => await Db.RemoveAsync<T>(id);
    }
}