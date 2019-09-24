using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using SqlRepository.Interfaces;
using UniversalWebApi.FilterModels;

namespace UniversalWebApi.Helpers.Filters
{
    public class ApiAsyncActionFilter : /*IAsyncResultFilter,*/ IAsyncActionFilter
    {
        private readonly IDataRepository _repository;

        public ApiAsyncActionFilter(IDataRepository repository)
        {
            _repository = repository;
        }
        
        
        
        
//        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
//        {
//            var result = new ActionResultModel
//            {
//                Action = context.RouteData.Values["action"].ToString(),
//                Controller = context.RouteData.Values["controller"].ToString(),
//                StatusCode = context.HttpContext.Response.StatusCode,
//                Result = context.HttpContext.Response.ToString()
//            };
//
//            await _repository.InsertAsync(result);
//        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var result = new ActionResultModel
            {
                Action = context.RouteData.Values["action"].ToString(),
                Controller = context.RouteData.Values["controller"].ToString(),
                StatusCode = context.HttpContext.Response.StatusCode,
                Result = context.HttpContext.Response.ToString()
            };

            await _repository.InsertAsync(result);
        }
    }
}