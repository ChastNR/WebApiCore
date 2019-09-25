using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SqlRepository.Interfaces;

namespace UniversalWebApi.Helpers.ExceptionManager
{
    public class ExceptionManager : IExceptionManager
    {
        private readonly IDataRepository _repository;
        private readonly ILogger<ExceptionManager> _logger;

        public ExceptionManager(IDataRepository repository, ILogger<ExceptionManager> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task Log(Exception exception)
        {
            try
            {
                await _repository.InsertAsync(new ExceptionContract
                {
                    Message = exception.Message
                });
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(e.HResult, "Database unavailable"), exception.Message);
            }
        }
    }
}