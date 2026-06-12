using CURE.Application.DTOs.Auth;

namespace CURE.Application.Interfaces.Services;

public interface IAuthService
{
    Task<AuthResponseDto> LoginAsync(LoginDto dto);

    Task<long> RegisterAsync(RegisterDto dto);
}