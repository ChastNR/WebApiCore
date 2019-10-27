using System;
using System.Threading.Tasks;
using AuthenticationProcessor.Contracts;
using AuthenticationProcessor.Interfaces;
using AuthenticationProcessor.UserData;
using DataRepository.Contracts;
using DataRepository.Interfaces;
using Microsoft.AspNetCore.Http;
using Tools.Logger;

namespace AuthenticationProcessor
{
    public class AuthProcessor : IAuthProcessor
    {
        private readonly IUserRepository _sqlRepository;
        private readonly IHttpContextAccessor _context;
        private readonly IExceptionManager _manager;
        public AuthProcessor(IUserRepository sqlRepository, IHttpContextAccessor context, IExceptionManager manager)
        {
            _sqlRepository = sqlRepository;
            _context = context;
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
                var userAuthData = new UserAuthData
                {
                    LastUsedIp = _context.HttpContext.Connection.RemoteIpAddress.ToString(),
                    UserAgent = _context.HttpContext.Request.Headers["User-Agent"],
                };
                var userInsertId = await _sqlRepository.InsertUserAsyncWithReturnId(new User
                {
                    Name = contract.Name,
                    Email = contract.Email,
                    PhoneNumber = contract.PhoneNumber,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(contract.Password)
                });

                userAuthData.UserId = userInsertId;

                await _sqlRepository.InsertAsync(userAuthData);

                return true;
            }
            catch (Exception e)
            {
                await _manager.Log(e);
                return false;
            }
        }

        public async Task Login(LoginContract contract)
        {
            if (string.IsNullOrEmpty(contract.Login) || string.IsNullOrEmpty(contract.Password))
            {
                contract.Valid = false;
            }

            var user = await _sqlRepository.GetUserByEmailOrPhoneNumber(contract.Login);

            bool validPassword = BCrypt.Net.BCrypt.Verify(user.PasswordHash, contract.Password);

            if (!validPassword)
            {
                contract.Valid = false;
            }

            contract.Valid = true;
            contract.UserId = user.Id;
        }
    }
}