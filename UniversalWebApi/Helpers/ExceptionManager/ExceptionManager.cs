using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoRepository.Interfaces;
using SqlRepository.Interfaces;

namespace UniversalWebApi.Helpers.ExceptionManager
{
    public class ExceptionManager : IExceptionManager
    {
        private readonly IDataRepository _dataRepository;
        private readonly IMongoRepository _mongoRepository;
        private readonly ILogger<ExceptionManager> _logger;

        public ExceptionManager(IDataRepository dataRepository, IMongoRepository mongoRepository,
            ILogger<ExceptionManager> logger)
        {
            _dataRepository = dataRepository;
            _mongoRepository = mongoRepository;
            _logger = logger;
        }

        public async Task Log(Exception exception)
        {
            try
            {
                await _dataRepository.InsertAsync(new ExceptionContract
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
                await _dataRepository.InsertAsync(new ExceptionContract
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