using System;
using System.Threading.Tasks;
using AuthenticationProcessor.Contracts;
using AuthenticationProcessor.Interfaces;
using DataRepository.Contracts;
using DataRepository.Interfaces;
using Tools.Logger;
using static BCrypt.Net.BCrypt;

namespace AuthenticationProcessor
{
    public class AuthProcessor : IAuthProcessor
    {
        private readonly IUserRepository _sqlRepository;
        private readonly IExceptionManager _manager;
        public AuthProcessor(IUserRepository sqlRepository, IExceptionManager manager)
        {
            _sqlRepository = sqlRepository;
            _manager = manager;
        }

        public async Task<bool> Register(RegistrationContract contract)
        {
            if (await _sqlRepository.AnotherUserWithSameProps(contract.Email, contract.PhoneNumber))
            {
                return false;
            }

            try
            {
                var user = new User
                {
                    Name = contract.Name,
                    Email = contract.Email,
                    PhoneNumber = contract.PhoneNumber,
                    PasswordHash = HashPassword(contract.Password)
                };

                var userInsertId = await _sqlRepository.InsertUserAsyncWithReturnId(user);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> Login(LoginContract contract)
        {
            if (string.IsNullOrEmpty(contract.Login) && string.IsNullOrEmpty(contract.Password))
            {
                throw new UnauthorizedAccessException();
            }

            var user = await _sqlRepository.GetUserByEmailOrPhoneNumber(contract.Login);

            bool validPassword = Verify(contract.Password, user.PasswordHash);

            if (!validPassword)
            {
                throw new UnauthorizedAccessException();
            }

            return user.Id.ToString();
        }
    }
}