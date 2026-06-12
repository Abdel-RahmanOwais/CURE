namespace CURE.Application.DTOs.Patients;

public class PatientResponseDto
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string BloodType { get; set; } = null!;

    public string EmergencyContact { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}