using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthenticationProcessor.Contracts;
using AuthenticationProcessor.Interfaces;
using AuthenticationProcessor.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace UniversalWebApi.Controllers.BaseControllers
{
    //[ServiceFilter(typeof(ApiExceptionFilter))]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private AuthOptions AuthOptions { get; }
        private readonly IAuthProcessor _processor;

        public AuthController(IOptions<AuthOptions> authOptions, IAuthProcessor processor)
        {
            AuthOptions = authOptions.Value;
            _processor = processor;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] LoginContract contract)
        {
            var result = await _processor.Login(contract);
            return !string.IsNullOrEmpty(result) ? (IActionResult)Ok(GetToken(result)) : BadRequest();
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] RegistrationContract contract)
        {
            var result = await _processor.Register(contract);
            return result ? (IActionResult)Ok() : BadRequest();
        }

        private string GetToken(string userId)
        {
            var signingCredentials =
                new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthOptions.SecurityKey)),
                    SecurityAlgorithms.HmacSha256Signature);

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userId)
            };

            var token = new JwtSecurityToken(
                AuthOptions.Issuer,
                AuthOptions.Audience,
                expires: DateTime.Now.AddMinutes(AuthOptions.LifeTime),
                signingCredentials: signingCredentials,
                claims: claims
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}