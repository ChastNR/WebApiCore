using System;
using System.Threading.Tasks;

using AuthenticationProcessor.Contracts;
using AuthenticationProcessor.Interfaces;

using DataAccess.Contracts;
using DataAccess.Interfaces;

using Tools.Logger;

using static BCrypt.Net.BCrypt;

namespace AuthenticationProcessor
{
    public class AuthProcessor : IAuthProcessor
    {
        private readonly IUserRepository _userRepository;
        private readonly IExceptionManager _manager;
        public AuthProcessor(IUserRepository userRepository, IExceptionManager manager)
        {
            _userRepository = userRepository;
            _manager = manager;
        }

        public async Task<bool> Register(RegistrationModel contract)
        {
            if (await _userRepository.AnotherUserWithSamePropsAsync(contract.Email, contract.PhoneNumber))
            {
                return false;
            }

            var user = new User
            {
                Name = contract.Name,
                Email = contract.Email,
                PhoneNumber = contract.PhoneNumber,
                PasswordHash = HashPassword(contract.Password)
            };

            await _userRepository.InsertUserWithReturnIdAsync(user);

            return true;
        }

        public async Task<string> Login(LoginModel contract)
        {
            if (string.IsNullOrEmpty(contract.Login) && string.IsNullOrEmpty(contract.Password))
            {
                throw new UnauthorizedAccessException();
            }

            var user = await _userRepository.GetUserByEmailOrPhoneNumberAsync(contract.Login);

            var validPassword = Verify(contract.Password, user.PasswordHash);

            if (!validPassword)
            {
                throw new UnauthorizedAccessException();
            }

            return user.Id.ToString();
        }
    }
}