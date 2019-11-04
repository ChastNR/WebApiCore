using System;
using System.Threading.Tasks;

namespace Tools.Logger
{
    public interface IExceptionManager
    {
        Task Log(ExceptionContract contract);
    }
}