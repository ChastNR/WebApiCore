using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using SqlRepository.Interfaces;
using UniversalWebApi.FilterModels;
using UniversalWebApi.Helpers.ExceptionManager;

namespace UniversalWebApi.Helpers.Filters
{
    public class ApiAsyncActionFilter : IAsyncResultFilter
    {
        private readonly IDataRepository _repository;
        private readonly IExceptionManager _exceptionManager;

        public ApiAsyncActionFilter(IDataRepository repository, IExceptionManager exceptionManager)
        {
            _repository = repository;
            _exceptionManager = exceptionManager;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            try
            {
                await _repository.InsertAsync(new ActionResultModel
                {
                    Action = context.RouteData.Values["action"].ToString(),
                    Controller = context.RouteData.Values["controller"].ToString(),
                    IpAddress =
                        $"{context.HttpContext.Connection.LocalIpAddress} {context.HttpContext.Connection.RemoteIpAddress}",
                    StatusCode = context.HttpContext.Response.StatusCode
                });

                await next();
            }
            catch (Exception exception)
            {
                await _exceptionManager.Log(exception);
                await next();
            }
        }
    }
}