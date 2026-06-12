using AutoMapper;
using CURE.Application.DTOs.Users;
using CURE.Domain.Entities;

namespace CURE.Application.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<Users, UserResponseDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Person.FullName))

                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Person.Gender))

                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Person.City))

                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Person.PhoneNumber))

                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Person.Address))

                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.Person.DateOfBirth));


            CreateMap<CreateUserDto, Users>()
              .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
              .ForMember(dest => dest.Person, opt => opt.Ignore());

            CreateMap<UpdateUserDto, Users>()
              .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
              .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
