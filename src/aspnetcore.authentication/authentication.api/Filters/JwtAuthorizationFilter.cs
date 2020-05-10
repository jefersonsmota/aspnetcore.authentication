using authentication.api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace authentication.api.Filters
{
    public class JwtAuthorizationFilter : IAsyncAuthorizationFilter
    {
        public AuthorizationPolicy Policy { get; }

        public JwtAuthorizationFilter(AuthorizationPolicy policy)
        {
            Policy = policy ?? throw new ArgumentNullException(nameof(policy));
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.ActionDescriptor.EndpointMetadata.Any(item => item is Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute))
            {
                return;

            }

            var policyEvaluator = context.HttpContext.RequestServices.GetRequiredService<IPolicyEvaluator>();
            var authenticateResult = await policyEvaluator.AuthenticateAsync(Policy, context.HttpContext);
            var authorizeResult = await policyEvaluator.AuthorizeAsync(Policy, authenticateResult, context.HttpContext, context);

            if (authorizeResult.Challenged)
            {
                // Return custom 401 result
                context.Result = new ObjectResult(new DataResponse("Unauthorized", 401));
            }
            else if (authorizeResult.Forbidden)
            {
                // Return default 403 result
                context.Result = new ObjectResult(new DataResponse("Unauthorized", 403));
            }
        }
    }
}
