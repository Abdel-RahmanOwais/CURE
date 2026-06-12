using AutoMapper;
using CURE.Application.DTOs.Patients;
using CURE.Domain.Entities;

namespace CURE.Application.Mappings
{
    public class PatientMappingProfile : Profile
    {
        public PatientMappingProfile()
        {
            CreateMap<Patient, PatientResponseDto>()
             .ForMember(
             dest => dest.FullName,
             opt => opt.MapFrom(src => src.User.Person.FullName))

            .ForMember(
             dest => dest.Email,
             opt => opt.MapFrom(src => src.User.Email));


            CreateMap<CreatePatientDto, Patient>();

            CreateMap<UpdatePatientDto, Patient>()
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, srcMember)
                        => srcMember != null));
        }
    }
}
