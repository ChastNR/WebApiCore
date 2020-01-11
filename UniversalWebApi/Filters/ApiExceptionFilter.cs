using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

using Tools.Logger;

namespace UniversalWebApi.Filters
{
    public class ApiExceptionFilterAttribute : Attribute, IAsyncExceptionFilter
    {
        private readonly ILogger<ApiExceptionFilterAttribute> _logger;
        private readonly IExceptionManager _manager;

        public ApiExceptionFilterAttribute(IExceptionManager manager, ILogger<ApiExceptionFilterAttribute> logger)
        {
            _manager = manager;
            _logger = logger;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}