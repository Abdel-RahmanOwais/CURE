using CURE.Application.DTOs.Nurses;

namespace CURE.Application.Interfaces.Services;

public interface INurseService
{
    Task<List<NurseResponseDto>> GetAllAsync(
        int pageNumber,
        int pageSize);

    Task<NurseResponseDto?> GetByIdAsync(long id);

    Task<NurseResponseDto?> GetByUserIdAsync(long userId);
    Task<long> CreateAsync(CreateNurseDto dto);

    Task UpdateAsync(long id, UpdateNurseDto dto);

    Task DeleteAsync(long id);
}