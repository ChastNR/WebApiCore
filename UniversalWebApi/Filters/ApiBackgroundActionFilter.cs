﻿using System;
using System.IO;
using System.Threading.Tasks;
using DataRepository.Interfaces.Base;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace UniversalWebApi.Filters
{
    public class ApiBackgroundActionFilter : IAsyncActionFilter
    {
        private readonly IMongoRepository _mr;
        private readonly ILogger<ApiBackgroundActionFilter> _logger;

        public ApiBackgroundActionFilter(IMongoRepository mr, ILogger<ApiBackgroundActionFilter> logger)
        {
            _mr = mr;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                var data = new TelemetryData
                {
                    RequestPath = context.HttpContext.Request.Path,
                    HttpMethod = context.HttpContext.Request.Method,
                    UserIp = $"{context.HttpContext.Connection.LocalIpAddress} {context.HttpContext.Connection.RemoteIpAddress}",
                    UserAgent = context.HttpContext.Request.Headers["User-Agent"]
            };

                await _mr.AddAsync(data);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
            }
            finally
            {
                await next();
            }
        }
    }

    public class TelemetryData : IMongoDoc
    {
        public ObjectId Id { get; set; }

        public DateTime RequestTime { get; set; } = DateTime.Now;

        public string RequestPath { get; set; }

        public string HttpMethod { get; set; }

        public string UserIp { get; set; }

        public string UserAgent { get; set; }
    }
}