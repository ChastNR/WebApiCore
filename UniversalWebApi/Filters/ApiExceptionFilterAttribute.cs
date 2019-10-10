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

            _manager.Log(exceptionContract).GetAwaiter().GetResult();

            //1) AddNewException(apiExceptionContract).GetAwaiter().GetResult();

            //2) var addTask = AddNewException(apiExceptionContract);
            //addTask.Wait();

            //3) var addTask2 = Task.Run(async () => await AddNewException(apiExceptionContract));
            //addTask2.Wait();
        }

        //private async Task AddNewException(ApiExceptionContract contract) => await _mongo.AddAsync(contract);
    }
}