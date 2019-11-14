using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

using DataRepository.Interfaces.Base;

namespace UniversalWebApi.Controllers.BaseControllers
{
    public abstract class MongoDbController<T> : BaseController<MongoDbController<T>, IMongoRepository> where T : class, IMongoDoc
    {
        protected MongoDbController(ILogger<MongoDbController<T>> logger, IMongoRepository service) : base(logger, service)
        {
        }

        [HttpGet]
        public Task<IEnumerable<T>> Get() => _service.GetAsync<T>();

        [HttpGet("{id}")]
        public Task<T> Get(ObjectId id) => _service.GetAsync<T>(id);

        [HttpPost]
        public Task Post([FromBody] T entity) => _service.AddAsync(entity);

        [HttpPut]
        public Task Put([FromBody] T entity) => _service.UpdateAsync(entity);

        [HttpDelete("{id}")]
        public Task Delete(ObjectId id) => _service.RemoveAsync<T>(id);
    }
}