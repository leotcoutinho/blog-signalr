using Datum.Blog.API.Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Datum.Blog.API.Configurations
{
    public static class JwtTokenSetup
    {
        private static string JwtKey { get; set; }

        #region Config JWT

        public static void JwtSetup(IServiceCollection services, IConfiguration configuration)
        {
            JwtKey = configuration.GetSection("JwtSettings:SecurityKey").Value;

            if (JwtKey == null)
            {
                throw new Exception("Security Key não existe nas configurações do projeto.");
            }

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("Token inválido : " + context.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("Token válido: " + context.SecurityToken);
                        return Task.CompletedTask;
                    }
                };
            });
        }

        #endregion

        #region Create Token
        public static string GenerateToken(Usuario user, IConfiguration configuration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new ("id", user.UsuarioId.ToString()),
                    new ("email", user.Email.ToString())
                }),
                Expires =  DateTime.UtcNow.AddMinutes(int.Parse(configuration.GetSection("JwtSettings:ExpireTimeMinutes").Value)),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        #endregion
    }
}
