using System.Collections.Generic;

namespace UnityApiController
{
    public interface IApiController
    {
        IEnumerable<T> Get<T>() where T : class;
        T Get<T>(object id) where T : class;
        bool Post<T>(T t) where T : class;
        bool Put<T>(T t) where T : class;
        bool Delete<T>(object id) where T : class;
    }
}