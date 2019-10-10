using System;
using System.Threading.Tasks;
using DataRepository.Interfaces.Base;
using Microsoft.AspNetCore.Mvc.Filters;
using Tools.Logger;

namespace UniversalWebApi.Filters
{
    public class ApiAsyncActionFilter : IAsyncResultFilter, IAsyncActionFilter
    {
        private readonly IExceptionManager _exceptionManager;
        public ApiAsyncActionFilter(IMongoRepository mongoRepository, IExceptionManager exceptionManager)
        {
            _exceptionManager = exceptionManager;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            try
            {
//                await _mongoRepository.AddAsync(new ActionResultModel
//                {
//                    Action = context.RouteData.Values["action"].ToString(),
//                    Controller = context.RouteData.Values["controller"].ToString(),
//                    IpAddress =
//                        $"{context.HttpContext.Connection.LocalIpAddress} {context.HttpContext.Connection.RemoteIpAddress}",
//                    StatusCode = context.HttpContext.Response.StatusCode
//                });

                await next();
            }
            catch (Exception exception)
            {
                await _exceptionManager.Log(exception);
                await next();
            }
        }

        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            throw new NotImplementedException();
        }
    }
}