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
                return BadRequest();
            }

            var token = GetToken(userId);

            return Ok(token);
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