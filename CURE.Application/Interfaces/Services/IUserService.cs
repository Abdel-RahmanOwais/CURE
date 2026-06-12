using CURE.Application.DTOs.Users;

namespace CURE.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<List<UserResponseDto>> GetAllAsync(int pageNumber, int pageSize);

        Task<UserResponseDto?> GetByIdAsync(long id, bool trackChanges = false);

        Task<UserResponseDto?> GetByEmailAsync(string email, bool trackChanges = false);

        Task<long> CreateAsync(CreateUserDto dto);

        Task UpdateAsync(long id, UpdateUserDto dto);

        Task SoftDelete(long id);
    }
}

