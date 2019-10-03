using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace MongoRepository.Interfaces
{
    public interface IMongoRepository
    {
        Task<IEnumerable<T>> GetAsync<T>() where T : class, IMongoDoc;
        Task<IEnumerable<T>> GetAsync2<T>() where T : class, IMongoDoc;
        Task<T> GetAsync<T>(ObjectId id) where T : class, IMongoDoc;
        Task AddAsync<T>(T t) where T : class, IMongoDoc;
        Task UpdateAsync<T>(T t) where T : class, IMongoDoc;
        Task RemoveAsync<T>(ObjectId id) where T : class, IMongoDoc;
    }
}