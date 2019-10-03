﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MongoDB.Bson;
using MongoRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace UniversalWebApi.Helpers.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        private readonly IMongoRepository _mDb;
        public ApiExceptionFilter(IMongoRepository mDb) => _mDb = mDb;

        public void OnException(ExceptionContext context)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var message = context.Exception.Message;
            var action = context.RouteData.Values["action"].ToString();
            var controller = context.RouteData.Values["controller"].ToString();

            var apiExceptionContract = new ApiExciptionContract
            {
                StatusCode = (int)statusCode,
                Message = message,
                Controller = controller,
                Action = action
            };

            var addTask = Task.Run(async () => await AddNewException(apiExceptionContract));
            addTask.Wait();
        }

        private async Task AddNewException(ApiExciptionContract contract) => await _mDb.AddAsync<ApiExciptionContract>(contract);
    }

    public class ApiExciptionContract : IMongoDoc
    {
        public ObjectId Id { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }

    }
}
