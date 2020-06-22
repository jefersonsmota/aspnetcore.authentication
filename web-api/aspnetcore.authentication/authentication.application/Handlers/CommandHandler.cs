using authentication.application.Common;
using authentication.application.Common.Interfaces;
using authentication.domain.Notifications;
using AutoMapper;

namespace authentication.application.Handlers
{
    public abstract class CommandHandler : IHttpCommandResponse
    {
        protected readonly IMapper _mapper;
        protected readonly NotificationContext _notificationContext;
        protected CommandHandler(IMapper mapper, NotificationContext notificationContext)
        {
            _mapper = mapper;
            _notificationContext = notificationContext;
        }

        public CommandResponse BadRequest(object response, string message = null)
        {
            var commandResponse = new CommandResponse(400, message, response, false);
            return commandResponse;
        }

        public CommandResponse Created(object response, string message = null)
        {
            var commandResponse = new CommandResponse(201, message, response);
            commandResponse.Success = true;
            return commandResponse;
        }

        public CommandResponse InternalError(object response, string message = null)
        {
            var commandResponse = new CommandResponse(500, message, response, false);
            return commandResponse;
        }

        public CommandResponse NotFound(object response, string message = null)
        {
            var commandResponse = new CommandResponse(404, message, response, false);
            return commandResponse;
        }

        public CommandResponse Ok(object response, string message = null)
        {
            var commandResponse = new CommandResponse(200, message, response);
            commandResponse.Success = true;
            return commandResponse;
        }
    }
}
