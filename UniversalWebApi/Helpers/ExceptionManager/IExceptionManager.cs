using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace UniversalWebApi.Helpers.ExceptionManager
{
    public interface IExceptionManager
    {
        Task Log(Exception exception);
        Task Log(Exception exception, string className, string methodName);
    }
}