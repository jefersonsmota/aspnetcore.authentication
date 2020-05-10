using authentication.application.Commands.User;
using authentication.application.Common.Services;
using authentication.application.Handlers.Interfaces;
using authentication.domain.Constants;
using authentication.domain.Entities;
using authentication.domain.Exceptions;
using authentication.infrastructure.Interfaces;
using AutoMapper;
using System;
using System.Threading.Tasks;

namespace authentication.application.Handlers.Services
{
    public class UserCommandHandler : BaseHandler, IUserCommandHandler
    {
        private readonly IUserRespository _userRepository;
        private readonly IUserValidationHandler _userValidationHandler;

        public UserCommandHandler(
            IUserRespository userRepository, 
            IMapper mapper, 
            IUserValidationHandler userValidationHandler) : base(mapper)
        {
            _userRepository = userRepository;
            _userValidationHandler = userValidationHandler;
        }
        
        public async Task<int> Handler(CreateUserRequest command)
        {
            if (!_userValidationHandler.Handler(command))
                throw new ValidationException(_userValidationHandler.ErrorMessage());

            if (await _userRepository.CheckAlreadyExist(command.Email))
                throw new ValidationException(Messages.EMAIL_ALREADY_EXISTS);

            var user = _mapper.Map<User>(command);

            user.CreatedAt = DateTime.Now;

            user.Salt = Guid.NewGuid().ToString();

            user.Password = HashPassService.GenerateSaltedHash(user.Password, user.Salt);

            return await _userRepository.Add(_mapper.Map<User>(user));
        }

        public async Task<UserResponse> Handler(SingInUserRequest singIn)
        {
            if (!_userValidationHandler.Handler(singIn))
                throw new ValidationException(_userValidationHandler.ErrorMessage());

            var user = await _userRepository.GetByEmail(singIn.Email);

            if (user == null)
                throw new NotFoundException(Messages.INVALID_EMAIL_OR_PASSWORD);

            var testPass = HashPassService.GenerateSaltedHash(singIn.Password, user.Salt);

            if (!HashPassService.CompareByteArrays(user.Password, testPass))
                throw new ValidationException(Messages.INVALID_EMAIL_OR_PASSWORD);

            _ = _userRepository.RegisterAccess(user.Id, DateTime.Now);

            return _mapper.Map<UserResponse>(user);
        }

        public async Task<UserResponse> Handler(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
                throw new ValidationException(Messages.MISSING_FIELDS);

            if (login.Contains("@"))
                throw new ValidationException(Messages.INVALID_FIELDS);

            var user = await _userRepository.GetByEmail(login);
            return _mapper.Map<UserResponse>(user);
        }
    }
}
