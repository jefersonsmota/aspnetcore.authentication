using authentication.application.Commands.User;
using authentication.application.Common;
using authentication.application.Handlers.Interfaces;
using authentication.domain.Constants;
using authentication.domain.Entities;
using authentication.domain.Notifications;
using authentication.infrastructure.Interfaces;
using AutoMapper;
using System.Threading.Tasks;

namespace authentication.application.Handlers.Services
{
    public class UserCommandHandler : CommandHandler, IUserCommandHandler
    {
        private readonly IUserRespository _userRepository;

        public UserCommandHandler(
            IUserRespository userRepository, 
            IMapper mapper, 
            NotificationContext notificationContext) : base(mapper, notificationContext)
        {
            _userRepository = userRepository;
        }
        
        public async Task<CommandResponse> Handler(CreateUserRequest command)
        {
            var user = _mapper.Map<CreateUserRequest, User>(command);

            if(!user.IsValid())
            {
                _notificationContext.AddNotifications(user.Validation);
                return BadRequest(null, Messages.INVALID_FIELDS);
            }

            if (await _userRepository.CheckAlreadyExist(user.Email))
            {
                _notificationContext.AddNotification("Email", Messages.EMAIL_ALREADY_EXISTS);
                return BadRequest(null, Messages.INVALID_FIELDS);
            }

            return Created(await _userRepository.Add(user), Messages.CREATED_SUCCESS);
        }

        public async Task<CommandResponse> Handler(SingInUserRequest singIn)
        {
            if(string.IsNullOrEmpty(singIn?.Email) || string.IsNullOrWhiteSpace(singIn?.Password))
            {
                _notificationContext.AddNotification("SingIn", Messages.MISSING_FIELDS);
                return BadRequest(null, Messages.MISSING_FIELDS);
            }

            var user = await _userRepository.GetByEmail(singIn.Email);

            if (user == null || !user.isValidPass(singIn.Password))
            {
                _notificationContext.AddNotification("SingIn", Messages.INVALID_EMAIL_OR_PASSWORD);
                return NotFound(null, Messages.INVALID_EMAIL_OR_PASSWORD);
            }

            user.UpdateLastLogin();

            _ = _userRepository.RegisterAccess(user);

            return Ok(_mapper.Map<UserResponse>(user), Messages.SING_IN);
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
