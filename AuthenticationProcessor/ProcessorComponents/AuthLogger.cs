using DataRepository.Interfaces.Base;

namespace AuthenticationProcessor.ProcessorComponents
{
    public class AuthLogger
    {
        private readonly IMongoRepository _repo;
        public AuthLogger(IMongoRepository repo) => _repo = repo;

        public void Log()
        {

        }
    }
}