using System.Collections.Generic;
 using System.Threading.Tasks;
 
 namespace UnityApiController
 {
     public interface IApiController
     {
         Task<IEnumerable<T>> Get<T>() where T : class;
         T Get<T>(object id) where T : class;
         Task Send<T>(T t) where T : class;
     }
 }