using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthenticationProcessor.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationProcessor.ProcessorComponents
{
    public class JwtComponent
    {
        private AuthOptions AuthOptions { get; }

        public JwtComponent(IOptions<AuthOptions> authOptions)
        {
            AuthOptions = authOptions.Value;
        }

        public string GetToken(string id)
        {
            var signingCredentials =
                new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthOptions.SecurityKey)),
                    SecurityAlgorithms.HmacSha256Signature);

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, id)
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