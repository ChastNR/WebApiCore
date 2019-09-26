using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using SqlRepository.Interfaces;
using UniversalWebApi.FilterModels;

namespace UniversalWebApi.Helpers.Filters
{
    public class ApiAsyncActionFilter : IAsyncResultFilter
    {
        private readonly IDataRepository _repository;
        private readonly ILogger<ApiAsyncActionFilter> _logger;

        public ApiAsyncActionFilter(IDataRepository repository, ILogger<ApiAsyncActionFilter> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var originalBody = context.HttpContext.Response.Body;

            await using (var memStream = new MemoryStream())
            {
                context.HttpContext.Response.Body = memStream;

                memStream.Position = 0;

                await _repository.InsertAsync(new ActionResultModel
                {
                    Action = context.RouteData.Values["action"].ToString(),
                    Controller = context.RouteData.Values["controller"].ToString(),
                    StatusCode = context.HttpContext.Response.StatusCode,
                    Result = new StreamReader(memStream).ReadToEnd()
                });

                memStream.Position = 0;
                await memStream.CopyToAsync(originalBody);
            }

            await next();
        }
    }
}