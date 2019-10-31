using System.Collections.Generic;
using System.Threading.Tasks;
using DataRepository.Interfaces.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using UniversalWebApi.Filters;

namespace UniversalWebApi.Controllers.BaseControllers
{
    [ServiceFilter(typeof(ApiExceptionFilter))]
    public abstract class MongoDbController<T> : Controller where T : class, IMongoDoc
    {
        private IMongoRepository Db => HttpContext.RequestServices.GetRequiredService<IMongoRepository>();

        [HttpGet]
        public Task<IEnumerable<T>> Get() => Db.GetAsync<T>();

        [HttpGet("{id}")]
        public Task<T> Get(ObjectId id) => Db.GetAsync<T>(id);

        [HttpPost]
        public Task Post([FromBody] T entity) => Db.AddAsync(entity);

        [HttpPut]
        public Task Put([FromBody] T entity) => Db.UpdateAsync(entity);

        [HttpDelete("{id}")]
        public Task Delete(ObjectId id) => Db.RemoveAsync<T>(id);
    }
}