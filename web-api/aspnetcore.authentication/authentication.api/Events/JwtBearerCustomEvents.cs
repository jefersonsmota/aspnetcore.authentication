using authentication.application.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace authentication.api.Events
{
    public class JwtBearerCustomEvents : JwtBearerEvents
    {
        public override Task AuthenticationFailed(AuthenticationFailedContext context)
        {
            if(context.Exception is SecurityTokenInvalidLifetimeException)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json";
                
                context.Response.WriteAsync(
                    Newtonsoft.Json.JsonConvert.SerializeObject(
                        new CommandResponse(statusCode: StatusCodes.Status403Forbidden, message: "Unauthorized - invalid session", null, false) 
                        )
                    );
            }

            return base.AuthenticationFailed(context);
        }

        public override Task Challenge(JwtBearerChallengeContext context)
        {
            if (context.AuthenticateFailure == null && context.Error == "invalid_token")
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                context.Response.WriteAsync(
                    Newtonsoft.Json.JsonConvert.SerializeObject(
                        new CommandResponse(statusCode: StatusCodes.Status401Unauthorized, message: "Unauthorized", null, false)
                        )
                    );
            }

            return base.Challenge(context);
        }
    }
}
