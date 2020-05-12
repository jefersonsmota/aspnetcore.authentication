using AutoMapper;

namespace authentication.application.Handlers
{
    public abstract class BaseHandler
    {
        protected readonly IMapper _mapper;
        public string ErrorMessage { get; protected set; }
        public bool IsValid { get; protected set; }
        protected BaseHandler(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
