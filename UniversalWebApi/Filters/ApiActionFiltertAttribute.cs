using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using DataRepository.Interfaces.Base;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Tools.Logger;
using UniversalWebApi.Filters.Contracts;

namespace UniversalWebApi.Filters
{
    public class ApiAsyncActionFilterAttribute : Attribute, IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            try
            {
                var mongoRepository = context.HttpContext.RequestServices.GetRequiredService<IMongoRepository>();

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

                await mongoRepository.AddAsync(actionResult);
            }
            catch (Exception exception)
            {
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<ApiAsyncActionFilterAttribute>>();
                logger.Log(LogLevel.Error, exception.Message);
            }
        }
    }
}