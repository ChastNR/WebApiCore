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

namespace UniversalWebApi.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private AuthOptions AuthOptions { get; }
        private readonly IAuthProcessor _processor;

        public AuthController(IOptions<AuthOptions> authOptions, IAuthProcessor processor)
        {
            AuthOptions = authOptions.Value ?? throw new ArgumentNullException(nameof(authOptions));
            _processor = processor ?? throw new ArgumentNullException(nameof(processor));
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] LoginContract contract)
        {
            var userId = await _processor.Login(contract);

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

            var result = await _processor.Register(contract);

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