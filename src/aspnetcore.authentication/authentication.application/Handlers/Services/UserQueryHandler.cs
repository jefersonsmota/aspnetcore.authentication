using authentication.application.Commands.User;
using authentication.application.Handlers.Interfaces;
using authentication.domain.Constants;
using authentication.domain.Exceptions;
using authentication.infrastructure.Interfaces;
using AutoMapper;
using System.Threading.Tasks;

namespace authentication.application.Handlers.Services
{
    public class UserQueryHandler : BaseHandler, IUserQueryHandler
    {
        private readonly IUserRespository _userRepository;
        public UserQueryHandler(IUserRespository userRepository, IMapper mapper) : base(mapper)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse> Handler(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
                throw new ValidationException("Missing fields");

            var user = await _userRepository.GetByEmail(login);
            if (user == null)
                throw new NotFoundException(Messages.NOT_FOUND_USER);


            return _mapper.Map<UserResponse>(user);
        }
    }
}
