using authentication.application.Commands.Phone;
using authentication.application.Commands.User;
using authentication.domain.Entities;
using AutoMapper;
using System.Linq;

namespace authentication.application.Common.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<CreateUserRequest, User>();
            this.CreateMap<CreatePhoneRequest, Phone>();
            this.CreateMap<User, UserResponse>()
                    .ForMember(dest => dest.Phones,
                                opt => opt.MapFrom(src => src.Phones.Select(x => $"{x.CountryCode} ({x.AreaCode}) {x.Number}"))
                                );
        }
    }
}
