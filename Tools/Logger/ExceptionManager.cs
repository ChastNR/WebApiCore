using System;
using System.Threading.Tasks;
using DataRepository.Interfaces.Base;

namespace Tools.Logger
{
    public class ExceptionManager : IExceptionManager
    {
        private readonly IMongoRepository _mongoRepository;
        public ExceptionManager(IMongoRepository mongoRepository) => _mongoRepository = mongoRepository;

        public async Task Log(ExceptionContract contract) => await _mongoRepository.AddAsync(contract);

        public async Task Log(Exception exception) => await _mongoRepository.AddAsync(new ExceptionContract
        {
            Message = exception.Message,
            Method = exception.TargetSite.Name
        });

        public async Task Log(Exception exception, string className, string methodName) =>
            await _mongoRepository.AddAsync(new ExceptionContract
            {
                Message = exception.Message,
                Class = className,
                Method = methodName,
            });
    }
}