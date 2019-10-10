using System;
using System.Threading.Tasks;
using AuthenticationProcessor.Contracts;
using AuthenticationProcessor.Interfaces;
using AuthenticationProcessor.ProcessorComponents;
using AuthenticationProcessor.Settings;
using DataRepository.Interfaces;
using DataRepository.Interfaces.Base;

namespace AuthenticationProcessor
{
    public class AuthProcessor : IAuthProcessor
    {
        private readonly IUserRepository _sqlRepository;
        private readonly AuthLogger _logger;

        public AuthProcessor(IUserRepository sqlRepository, IMongoRepository repo)
        {
            _sqlRepository = sqlRepository;
            _logger = new AuthLogger(repo);
        }

//        public async Task Login(LoginDataContract data)
//        {
//            await null;
//        }

        public async Task<bool> Register(RegistrationContract contract)
        {
            if (await ProcessRegistrationData(contract.Email, contract.PhoneNumber))
            {
                _logger.Log();
                return false;
            }
            
            
            
            return true;
        }

        private async Task<bool> ProcessRegistrationData(string email, string phoneNumber)
        {
            var user = await _sqlRepository.GetUserWithConditionAsync(email, phoneNumber);
            return user != null;
        }

    }
}