using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnityApiController
{
    public interface IApiController
    {
        Task <IEnumerable<T>> GetAsync<T>(string path) where T : class;
        
        Task<T> GetAsync<T>(string path, object id) where T : class;
        
        Task<bool> PostAsync<T>(string path, T t) where T : class;
        
        Task<bool> PutAsync<T>(string path, T t) where T : class;
        
        Task<bool> DeleteAsync<T>(string path, object id) where T : class;
    }
}