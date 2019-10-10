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
        
        //JWT Startup Config
        /*
        services.Configure<AuthOptions>(Configuration.GetSection("AuthOptions"));

        var authConfig = Configuration.GetSection("AuthOptions").Get<AuthOptions>();

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfig.SecurityKey));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                //what to validate
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                //setup validate data
                ValidIssuer = authConfig.Issuer,
                ValidAudience = authConfig.Audience,
                IssuerSigningKey = symmetricSecurityKey,
                ClockSkew = TimeSpan.Zero
            };
        });
        */
    }
}