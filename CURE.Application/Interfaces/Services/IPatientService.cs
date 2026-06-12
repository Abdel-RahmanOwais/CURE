using CURE.Application.DTOs.Patients;

namespace CURE.Application.Interfaces.Services;

public interface IPatientService
{
    Task<List<PatientResponseDto>> GetAllAsync(
        int pageNumber,
        int pageSize);

    Task<PatientResponseDto?> GetByIdAsync(long id);

    Task<long> CreateAsync(CreatePatientDto dto);

    Task UpdateAsync(long id, UpdatePatientDto dto);

    Task DeleteAsync(long id);
}