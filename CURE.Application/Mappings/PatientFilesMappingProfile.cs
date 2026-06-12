using AutoMapper;
using CURE.Domain.Entities;
namespace CURE.Application.Mappings
{
    public class PatientFilesMappingProfile : Profile
    {
        public PatientFilesMappingProfile()
        {

            CreateMap<PatientFile, PatientFilesMappingProfile>();
        }
    }
}
