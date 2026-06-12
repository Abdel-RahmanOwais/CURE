namespace CURE.Application.DTOs.Nurses;

public class CreateNurseDto
{
    public string FullName { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string Gender { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string Specialization { get; set; } = null!;

    public string LicenseNumber { get; set; } = null!;

    public int YearsOfExperience { get; set; }
}