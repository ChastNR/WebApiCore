using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using MongoDB.Bson;

using DataAccess.Interfaces.Base;

namespace UniversalWebApi.Controllers
{
    public abstract class MongoDbController<T> : ApiBaseController<MongoDbController<T>, IMongoRepository> 
        where T : class, IMongoDoc
    {
        protected MongoDbController(ILogger<MongoDbController<T>> logger, IMongoRepository service) 
            : base(logger, service)
        {
        }

        [HttpGet]
        public Task<IEnumerable<T>> Get()
        {
            return _service.GetAsync<T>();
        }

        [HttpGet("{id}")]
        public Task<T> Get(ObjectId id)
        {
            return _service.GetAsync<T>(id);
        }

        [HttpPost]
        public Task Post([FromBody] T entity)
        {
            return _service.AddAsync(entity);
        }

        [HttpPut]
        public Task Put([FromBody] T entity)
        {
            return _service.UpdateAsync(entity);
        }

        [HttpDelete("{id}")]
        public Task Delete(ObjectId id)
        {
            return _service.RemoveAsync<T>(id);
        }
    }
}