using CURE.Domain.Entities;

public interface IJwtProvider
{
    string GenerateToken(Users user);
}