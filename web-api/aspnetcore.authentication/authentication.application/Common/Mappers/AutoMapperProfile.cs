using authentication.application.Commands.Phone;
using authentication.application.Commands.User;
using authentication.domain.Entities;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace authentication.application.Common.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.ShouldUseConstructor = ci => true;

            this.CreateMap<CreatePhoneRequest, Phone>()
                .ConstructUsing(c => new Phone(c.Number, c.AreaCode, c.CountryCode));

            this.CreateMap<CreateUserRequest, User>()
                .ConvertUsing((c, u, v) => new User(c.FirstName,
                                         c.LastName,
                                         c.Email,
                                         c.Password,
                                         c.Hometown,
                                         v.Mapper.Map<IEnumerable<Phone>>(c.Phones)));

            this.CreateMap<User, UserResponse>()
                    .ForMember(dest => dest.Phones,
                                opt => opt.MapFrom(src => src.Phones.Select(x => $"{x.CountryCode} ({x.AreaCode}) {x.Number}"))
                                );
        }
    }
}
