using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DataRepository.Interfaces.Base
{
    public interface IMongoRepository
    {
        Task<IEnumerable<T>> GetAsync<T>() where T : class, IMongoDoc;
        Task<T> GetAsync<T>(ObjectId id) where T : class, IMongoDoc;
        Task AddAsync<T>(T t) where T : class, IMongoDoc;
        Task UpdateAsync<T>(T t) where T : class, IMongoDoc;
        Task RemoveAsync<T>(ObjectId id) where T : class, IMongoDoc;
    }
}