using System;
using System.Threading.Tasks;

namespace Tools.Logger
{
    public interface IExceptionManager
    {
        Task Log(ExceptionContract contract);
        Task Log(Exception exception);
        Task Log(Exception exception, string className, string methodName);
    }
}