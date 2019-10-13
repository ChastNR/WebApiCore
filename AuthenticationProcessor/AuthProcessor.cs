using System.Threading.Tasks;
using AuthenticationProcessor.Contracts;
using AuthenticationProcessor.Interfaces;
using AuthenticationProcessor.UserData;
using DataRepository.Contracts;
using DataRepository.Interfaces;
using Microsoft.AspNetCore.Http;

namespace AuthenticationProcessor
{
    public class AuthProcessor : IAuthProcessor
    {
        private readonly IUserRepository _sqlRepository;
        private readonly IHttpContextAccessor _context;

        public AuthProcessor(IUserRepository sqlRepository, IHttpContextAccessor context)
        {
            _sqlRepository = sqlRepository;
            _context = context;
        }

        public async Task<bool> Register(RegistrationContract contract)
        {
            if (await _sqlRepository.AnotherUserWithSameProps(contract.Email, contract.PhoneNumber)) return false;

            var userAuthData = new UserAuthData
            {
                LastUsedIp = _context.HttpContext.Connection.RemoteIpAddress.ToString(),
                UserAgent = _context.HttpContext.Request.Headers["User-Agent"],
            };

            var userInsertId = (int) await _sqlRepository.InsertAsyncWithReturnId(new User
            {
                Name = contract.Name,
                Email = contract.Email
            });

            userAuthData.UserId = userInsertId;

            await _sqlRepository.InsertAsync(userAuthData);

            return true;
        }
    }
}