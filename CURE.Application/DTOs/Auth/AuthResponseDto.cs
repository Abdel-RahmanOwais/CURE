namespace CURE.Application.DTOs.Auth;

public class AuthResponseDto
{
    public string Token { get; set; } = null!;

    public DateTime ExpireAt { get; set; }
}