using Microsoft.AspNetCore.Authentication.JwtBearer;
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
                context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(new ViewModels.DataResponse("Unauthorized - invalid session", StatusCodes.Status403Forbidden)));
            }

            return base.AuthenticationFailed(context);
        }

        public override Task Challenge(JwtBearerChallengeContext context)
        {
            if (context.AuthenticateFailure == null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(new ViewModels.DataResponse("Unauthorized", StatusCodes.Status401Unauthorized)));
            }

            return base.Challenge(context);
        }
    }
}
