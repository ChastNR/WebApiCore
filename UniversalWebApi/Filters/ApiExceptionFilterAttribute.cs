using System;
using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;
using Tools.Logger;

namespace UniversalWebApi.Filters
{
    public class ApiExceptionFilter : Attribute, IExceptionFilter
    {
        private readonly IExceptionManager _manager;
        public ApiExceptionFilter(IExceptionManager manager) => _manager = manager;

        public void OnException(ExceptionContext context)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var message = context.Exception.Message;
            var action = context.RouteData.Values["action"].ToString();
            var controller = context.RouteData.Values["controller"].ToString();

            var exceptionContract = new ExceptionContract()
            {
                StatusCode = (byte) statusCode,
                Message = message,
                Class = controller,
                Method = action
            };

            _manager.Log(exceptionContract).Wait();
        }
    }
}