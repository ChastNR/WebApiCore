using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace MongoRepository.Interfaces
{
    public interface IMongoRepository
    {
        IEnumerable<T> Get<T>() where T : class, IMongoDoc;

        T Get<T>(ObjectId id) where T : class, IMongoDoc;

        Task<T> GetAsync<T>(ObjectId id) where T : class, IMongoDoc;

        Task Add<T>(T t) where T : class, IMongoDoc;

        Task Update<T>(T t) where T : class, IMongoDoc;

        Task Remove<T>(ObjectId id) where T : class, IMongoDoc;
    }
}