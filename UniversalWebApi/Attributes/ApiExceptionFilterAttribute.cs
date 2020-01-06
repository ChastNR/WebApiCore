using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Filters;
using Tools.Logger;

namespace UniversalWebApi.Attributes
{
    public class ApiExceptionFilterAttribute : Attribute, IAsyncExceptionFilter
    {
        private readonly IExceptionManager _manager;

        public ApiExceptionFilterAttribute(IExceptionManager manager)
        {
            _manager = manager;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            var exception = new ExceptionContract
            {
                Method = context.HttpContext.Request.Method,
                Path = context.HttpContext.Request.Path,
                IpAddress =
                    $"{context.HttpContext.Connection.LocalIpAddress} {context.HttpContext.Connection.RemoteIpAddress}",
                Time = DateTime.Now,
                Message = context.Exception.Message,
            };

            await _manager.Log(exception);
        }
    }
}