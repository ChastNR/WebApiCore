using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Interfaces.Base
{
    public interface ISqlRepository
    {
        Task<IEnumerable<T>> GetAllAsync<T>() where T : class;
        
        Task<IEnumerable<T>> GetAllByConditionAsync<T>(string condition) where T : class;
        
        Task<T> GetAsync<T>(object id) where T : class;
        
        Task<T> GetByConditionAsync<T>(string condition) where T : class;
        
        Task InsertAsync<T>(T t) where T : class;
        
        /// <summary>
        /// Insert entity in database and return its id
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Entity id</returns>
        Task<int> InsertWithReturnIdAsync<T>(T t) where T : class;
        
        Task SaveRangeAsync<T>(IEnumerable<T> list) where T : class;
        
        Task UpdateAsync<T>(T t) where T : class;
        
        Task DeleteRowAsync<T>(object id) where T : class;
    }
}