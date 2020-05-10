using authentication.api.Configurations;
using authentication.api.ViewModels;
using authentication.application.Commands.User;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace authentication.api.Services
{
    public static class TokenService
    {
        public static SingInResponse GereneteToken(UserResponse user, AppSettings appSettings)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var hashKey = Encoding.ASCII.GetBytes(appSettings.Secret);
            var expires = DateTime.UtcNow.AddHours(appSettings.Expiration);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] { 
                   new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                   new Claim(JwtRegisteredClaimNames.Email, user.Email),
                   new Claim("Name", $"{user.FirstName} {user.LastName}")
                }),
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(hashKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

            var singInResponse = new SingInResponse
            {
                Sub = new
                {
                    user.Email,
                    Name = $"{user.FirstName} {user.LastName}"
                },
                ExpirationToken = expires,
                Token = tokenHandler.WriteToken(securityToken)
            };

            return singInResponse;
        }
    }
}
