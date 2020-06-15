using authentication.application.Common;
using authentication.domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace authentication.api.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception is HandlerException)
            {
                var excep = context.Exception as HandlerException;

                context.Result = new ObjectResult(new CommandResponse(excep.HttpStatusCode, excep.Message, null, false))
                {
                    StatusCode = excep.HttpStatusCode
                };
            }
            else
            {
                var details = new CommandResponse(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.", null, false);

                context.Result = new ObjectResult(details)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

            context.ExceptionHandled = true;

            return base.OnExceptionAsync(context);
        }
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is HandlerException)
            {
                var excep = context.Exception as HandlerException;

                context.Result = new ObjectResult(new CommandResponse(excep.HttpStatusCode, excep.Message, null, false))
                {
                    StatusCode = excep.HttpStatusCode
                };
            }
            else
            {
                var details = new CommandResponse(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.", null, false);

                context.Result = new ObjectResult(details)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

            context.ExceptionHandled = true;

            base.OnException(context);
        }
    }
}
