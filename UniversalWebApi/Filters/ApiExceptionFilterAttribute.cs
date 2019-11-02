using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Tools.Logger;

namespace UniversalWebApi.Filters
{
    public class ApiExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<IExceptionManager>();
            logger.Log(context.Exception).GetAwaiter().GetResult();
        }
    }
}