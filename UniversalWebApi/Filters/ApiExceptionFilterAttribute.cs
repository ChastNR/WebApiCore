using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Filters;

using MongoDB.Bson;

using Tools.Logger;

namespace UniversalWebApi.Filters
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
                StatusCode = context.HttpContext.Response.StatusCode,
                Time = DateTime.Now,
                Message = context.Exception.Message,
                Result = context.Result.ToJson()
            };

            await _manager.Log(exception);
        }
    }
}