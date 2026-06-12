using AutoMapper;
using CURE.Application.DTOs.Nurses;
using CURE.Domain.Entities;

namespace CURE.Application.Mappings.Nurses;

public class NurseMappingProfile : Profile
{
    public NurseMappingProfile()
    {
        CreateMap<Nurse, NurseResponseDto>()

            .ForMember(
                dest => dest.FullName,
                opt => opt.MapFrom(src => src.User.Person.FullName))

            .ForMember(
                dest => dest.Email,
                opt => opt.MapFrom(src => src.User.Email))

            .ForMember(
                dest => dest.PhoneNumber,
                opt => opt.MapFrom(src => src.User.Person.PhoneNumber))

            .ForMember(
                dest => dest.City,
                opt => opt.MapFrom(src => src.User.Person.City));



        CreateMap<CreateNurseDto, Nurse>();


        CreateMap<UpdateNurseDto, Nurse>()
            .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) =>
                    srcMember != null));
    }
}