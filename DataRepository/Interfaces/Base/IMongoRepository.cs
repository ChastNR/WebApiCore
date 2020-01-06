using System.Collections.Generic;
using System.Threading.Tasks;

using MongoDB.Bson;

namespace DataRepository.Interfaces.Base
{
    public interface IMongoRepository
    {
        /// <summary>
        /// Get all elements from Mongo collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Collection of elements</returns>
        Task<IEnumerable<T>> GetAsync<T>() where T : class, IMongoDoc;
        
        /// <summary>
        /// Get one element from collection by id
        /// </summary>
        /// <param name="id"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>T</returns>
        Task<T> GetAsync<T>(ObjectId id) where T : class, IMongoDoc;
        
        /// <summary>
        /// Add element to collection
        /// </summary>
        /// <param name="t"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task AddAsync<T>(T t) where T : class, IMongoDoc;
        
        /// <summary>
        /// Update element in collection
        /// </summary>
        /// <param name="t"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task UpdateAsync<T>(T t) where T : class, IMongoDoc;
        
        /// <summary>
        /// Delete element from collection
        /// </summary>
        /// <param name="id"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task RemoveAsync<T>(ObjectId id) where T : class, IMongoDoc;
    }
}