using CURE.Application.Interfaces.Security;

namespace CURE.Infrastructure.Security;

public class PasswordHasher : IPasswordHash
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool Verify(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}