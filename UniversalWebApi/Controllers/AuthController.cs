using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;

using AuthenticationProcessor.Contracts;
using AuthenticationProcessor.Interfaces;
using AuthenticationProcessor.Settings;
using UniversalWebApi.Controllers.BaseControllers;
using Microsoft.Extensions.Logging;

namespace UniversalWebApi.Controllers
{
    public class AuthController : BaseController<AuthController, IAuthProcessor>
    {
        private AuthOptions AuthOptions { get; }

        public AuthController(ILogger<AuthController> logger, IAuthProcessor processor, IOptions<AuthOptions> authOptions)
            : base(logger, processor)
        {
            AuthOptions = authOptions.Value ?? throw new ArgumentNullException(nameof(authOptions));
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] LoginContract contract)
        {
            var userId = await _service.Login(contract);

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Please, check your login or password");
            }

            var token = GetToken(userId);

            return Ok(new SignInReponse
            {
                Token = token,
                UserId = userId
            });
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] RegistrationContract contract)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please check your data");
            }

            var result = await _service.Register(contract);

            if (result != true)
            {
                return BadRequest();
            }

            return Ok();
        }

        private string GetToken(string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(AuthOptions.SecurityKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, userId)
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}