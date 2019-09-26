using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Abstractions;
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
                    Message = exception.Message,
                    Method = exception.TargetSite.Name
                });
            }
            catch (Exception e)
            {
                _logger.LogError(exception.Message);
            }
        }

        public async Task Log(Exception exception, string className, string methodName)
        {
            try
            {
                await _repository.InsertAsync(new ExceptionContract
                {
                    Message = exception.Message,
                    Class = className,
                    Method = methodName,
                });
            }
            catch (Exception e)
            {
                _logger.LogError(exception.Message);
            }
        }
    }
}