using System;
using System.Threading.Tasks;
using DataRepository.Interfaces.Base;
using Microsoft.Extensions.Logging;

namespace UniversalWebApi.Helpers.ExceptionManager
{
    public class ExceptionManager : IExceptionManager
    {
        private readonly IMongoRepository _mongoRepository;
        private readonly ILogger<ExceptionManager> _logger;

        public ExceptionManager(IMongoRepository mongoRepository,
            ILogger<ExceptionManager> logger)
        {
            _mongoRepository = mongoRepository;
            _logger = logger;
        }

        public async Task Log(Exception exception)
        {
            try
            {
                await _mongoRepository.AddAsync(new MExceptionContract
                {
                    Message = exception.Message,
                    Method = exception.TargetSite.Name
                });
            }
            catch (Exception)
            {
                _logger.LogError(exception.Message);
            }
        }

        public async Task Log(Exception exception, string className, string methodName)
        {
            try
            {
                await _mongoRepository.AddAsync(new MExceptionContract
                {
                    Message = exception.Message,
                    Class = className,
                    Method = methodName,
                });
            }
            catch (Exception)
            {
                _logger.LogError(exception.Message);
            }
        }
    }
}