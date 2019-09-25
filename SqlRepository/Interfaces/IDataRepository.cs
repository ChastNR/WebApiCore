using System.Collections.Generic;
using System.Threading.Tasks;

namespace SqlRepository.Interfaces
{
    public interface IDataRepository
    {
        IEnumerable<T> GetAll<T>()
            where T : class;

        Task<IEnumerable<T>> GetAllAsync<T>()
            where T : class;

        T Get<T>(int id)
            where T : class;

        Task<T> GetAsync<T>(int id)
            where T : class;

        T Get<T>(string condition)
            where T : class;

        Task InsertAsync<T>(T t)
            where T : class;

        Task<int> SaveRangeAsync<T>(IEnumerable<T> list)
            where T : class;

        Task UpdateAsync<T>(T t)
            where T : class;

        Task DeleteRowAsync<T>(int id)
            where T : class;
    }
}