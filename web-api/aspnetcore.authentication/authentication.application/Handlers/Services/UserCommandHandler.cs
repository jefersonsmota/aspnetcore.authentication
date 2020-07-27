using authentication.application.Commands.Request.User;
using authentication.application.Commands.User;
using authentication.application.Common;
using authentication.application.Handlers.Interfaces;
using authentication.domain.Constants;
using authentication.domain.Entities;
using authentication.domain.Notifications;
using authentication.domain.Services;
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

        public async Task<CommandResponse> Handler(MeRequest me)
        {
            if (string.IsNullOrWhiteSpace(me.Login))
            {
                _notificationContext.AddNotification("Login", Messages.MISSING_FIELDS);
                return BadRequest(me.Login, Messages.MISSING_FIELDS);
            }                

            var user = await _userRepository.GetByEmail(me.Login);
            if (user == null)
            {
                _notificationContext.AddNotification("Login", Messages.NOT_FOUND_USER);
                return NotFound(me.Login, Messages.NOT_FOUND_USER);
            }

            return Ok(_mapper.Map<UserResponse>(user), null);
        }

        public async Task<CommandResponse> Handler(ForgotPasswordRequest forgot)
        {
            if (string.IsNullOrWhiteSpace(forgot.Email))
            {
                _notificationContext.AddNotification("Email", Messages.MISSING_FIELDS);
                return BadRequest(forgot.Email, Messages.MISSING_FIELDS);
            }

            if (!await _userRepository.CheckAlreadyExist(forgot.Email))
            {
                _notificationContext.AddNotification("Email", Messages.NOT_FOUND_USER);
                return NotFound(forgot.Email, Messages.NOT_FOUND_USER);
            }

            MailService.Email(
                forgot.Email, 
                Templates.FORGOT_PASSWORD_SUBJECT, 
                Templates.FORGOT_PASSWORD_BODY
                            .Replace("{EMAIL}", forgot.Email)
                            .Replace("{URL}", ""), 
                true);

            return null;
        }
    }
}
