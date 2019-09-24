using System;
using System.Threading.Tasks;
using SqlRepository.Interfaces;

namespace UniversalWebApi.Helpers.ExceptionManager
{
    public class ExceptionManager : IExceptionManager
    {
        private readonly IDataRepository _repository;

        public ExceptionManager(IDataRepository repository)
        {
            _repository = repository;
        }

        public async Task Log(Exception exception)
        {
            await _repository.InsertAsync(new ExceptionContract
            {
                Message = exception.Message
            });
        }
    }
}