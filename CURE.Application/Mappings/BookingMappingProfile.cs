using AutoMapper;
using CURE.Application.DTOs.Booking;
using CURE.Domain.Entities;

namespace CURE.Application.Mappings
{
    public class BookingMappingProfile : Profile
    {
        public BookingMappingProfile()
        {
            CreateMap<Booking, BookingResponseDto>()

           .ForMember(
               dest => dest.PatientName,
               opt => opt.MapFrom(
                   src => src.Patient.User.Person.FullName))

           .ForMember(
               dest => dest.NurseName,
               opt => opt.MapFrom(
                   src => src.Nurse.User.Person.FullName))

           .ForMember(
               dest => dest.Status,
               opt => opt.MapFrom(
                   src => src.BookingStatus.Name));
        }
    }
}
