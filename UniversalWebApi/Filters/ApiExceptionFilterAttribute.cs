using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using Tools.Logger;

namespace UniversalWebApi.Filters
{
    public class ApiExceptionFilterAttribute : Attribute, IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<IExceptionManager>();

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

            await logger.Log(exception);
        }
    }
}