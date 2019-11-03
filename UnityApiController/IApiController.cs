using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnityApiController
{
    public interface IApiController
    {
        Task <IEnumerable<T>> Get<T>() where T : class;
        Task<T> Get<T>(object id) where T : class;
        Task<bool> Post<T>(T t) where T : class;
        Task<bool> Put<T>(T t) where T : class;
        Task<bool> Delete<T>(object id) where T : class;
    }
}