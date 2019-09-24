using System;
using System.Threading.Tasks;

namespace UniversalWebApi.Helpers.ExceptionManager
{
    public interface IExceptionManager
    {
        Task Log(Exception exception);
    }
}