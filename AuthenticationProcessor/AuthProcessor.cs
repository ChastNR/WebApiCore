using System;
using System.Threading.Tasks;
using AuthenticationProcessor.Contracts;
using AuthenticationProcessor.Interfaces;
using AuthenticationProcessor.UserData;
using DataRepository.Contracts;
using DataRepository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
            if (await _sqlRepository.AnotherUserWithSameProps(contract.Email, contract.PhoneNumber)) return false;

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
                    Email = contract.Email
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

        public async Task<T> Login<T>(LoginContract contract) where T : Type
        {
            var user = _sqlRepository.GetUserByEmailOrPhoneNumber(contract.Login);
        }
    }
}