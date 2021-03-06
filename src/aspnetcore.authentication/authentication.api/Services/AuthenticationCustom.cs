﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace authentication.api.Services
{
    public class CustomAuthenticationHandler : AuthenticationHandler<JwtBearerOptions>
    {
        private readonly IConfiguration _configuration;
        private SecurityToken _securityToken;
        public CustomAuthenticationHandler(
            IOptionsMonitor<JwtBearerOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IConfiguration configuration)
            : base(options, logger, encoder, clock)
        {
            _configuration = configuration;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            return await Task.Run(() =>
             {
                 if (!Request.Headers.ContainsKey("Authorization"))
                     return AuthenticateResult.Fail("Unauthorized");

                 string authorizationHeader = Request.Headers["Authorization"];
                 if (string.IsNullOrEmpty(authorizationHeader))
                 {
                     return AuthenticateResult.NoResult();
                 }

                 if (!authorizationHeader.StartsWith("bearer", StringComparison.OrdinalIgnoreCase))
                 {
                     return AuthenticateResult.Fail("Unauthorized");
                 }

                 string token = authorizationHeader.Substring("bearer".Length).Trim();

                 if (string.IsNullOrEmpty(token))
                 {
                     return AuthenticateResult.Fail("Unauthorized");
                 }

                 try
                 {
                     return validateToken(token);

                 }
                 catch(Exception ex)
                 {
                     return AuthenticateResult.Fail(ex);
                 }
             });
        }

        private AuthenticateResult validateToken(string token)
        {
            var key = _configuration["AppSettings:Secret"].ToString();
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            var validatedToken = handler.ValidateToken(token, validations, out _securityToken);
            if (validatedToken == null)
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            var principal = new System.Security.Principal.GenericPrincipal(validatedToken.Identity, null);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}
