using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataRepository.Interfaces.Base
{
    public interface ISqlRepository
    {
        Task<IEnumerable<T>> GetAllAsync<T>() where T : class;
        Task<T> GetAsync<T>(object id) where T : class;
        Task InsertAsync<T>(T t) where T : class;
        Task<object> InsertAsyncWithReturnId<T>(T t) where T : class;
        Task SaveRangeAsync<T>(IEnumerable<T> list) where T : class;
        Task UpdateAsync<T>(T t) where T : class;
        Task DeleteRowAsync<T>(object id) where T : class;
    }
}