using System;
using System.IO;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

using UniversalWebApi.Filters.Contracts;

using DataRepository.Interfaces.Base;

namespace UniversalWebApi.Filters
{
    public class ApiAsyncActionFilterAttribute : Attribute, IAsyncResultFilter
    {
        public readonly IMongoRepository _mongoRepository;
        public readonly ILogger<ApiAsyncActionFilterAttribute> _logger;

        public ApiAsyncActionFilterAttribute(IMongoRepository mongoRepository, ILogger<ApiAsyncActionFilterAttribute> logger)
        {
            _mongoRepository = mongoRepository;
            _logger = logger;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            try
            {
                string result;

                using (var reader = new StreamReader(context.HttpContext.Response.Body))
                {
                    reader.BaseStream.Seek(0, SeekOrigin.Begin);
                    result = reader.ReadToEnd();
                }

                var actionResult = new ActionResultContract
                {
                    Method = context.HttpContext.Request.Method,
                    Path = context.HttpContext.Request.Path,
                    IpAddress =
                        $"{context.HttpContext.Connection.LocalIpAddress} {context.HttpContext.Connection.RemoteIpAddress}",
                    StatusCode = context.HttpContext.Response.StatusCode,
                    Time = DateTime.Now,
                    Result = result
                };

                await _mongoRepository.AddAsync(actionResult);
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, exception.Message);
            }
        }
    }
}