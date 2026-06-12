namespace CURE.Application.DTOs.Auth;

public class RegisterDto
{
    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Address { get; set; } = null!;
    public DateOnly? DateOfBirth { get; set; }
    public string BloodType { get; set; }

    public string EmergencyContact { get; set; }
}