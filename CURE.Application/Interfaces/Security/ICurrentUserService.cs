namespace CURE.Application.Interfaces.Security
{
    public interface ICurrentUserService
    {
        long UserId { get; }

        string? Email { get; }

        bool IsAdmin { get; }

        bool IsNurse { get; }

        bool IsPatient { get; }
    }
}
