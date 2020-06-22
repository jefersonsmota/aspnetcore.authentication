using authentication.application.Commands.User;
using authentication.application.Common;
using authentication.application.Handlers.Interfaces;
using authentication.domain.Constants;
using authentication.domain.Notifications;
using authentication.infrastructure.Interfaces;
using AutoMapper;
using System.Threading.Tasks;

namespace authentication.application.Handlers.Services
{
    public class UserQueryHandler : CommandHandler, IUserQueryHandler
    {
        private readonly IUserRespository _userRepository;
        public UserQueryHandler(IUserRespository userRepository, IMapper mapper, NotificationContext notificationContext) : base(mapper, notificationContext)
        {
            _userRepository = userRepository;
        }

        public async Task<CommandResponse> Handler(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                _notificationContext.AddNotification("Login", Messages.MISSING_FIELDS);
                return BadRequest(login, Messages.MISSING_FIELDS);
            }

            var user = await _userRepository.GetByEmail(login);
            if (user == null)
            {
                _notificationContext.AddNotification("Login", Messages.NOT_FOUND_USER);
                return NotFound(login, Messages.NOT_FOUND_USER);
            }

            return Ok(_mapper.Map<UserResponse>(user), null);
        }
    }
}
