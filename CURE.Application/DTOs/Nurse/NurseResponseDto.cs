namespace CURE.Application.DTOs.Nurses;

public class NurseResponseDto
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Specialization { get; set; } = null!;

    public string LicenseNumber { get; set; } = null!;

    public int YearsOfExperience { get; set; }

    public DateTime CreatedAt { get; set; }
}