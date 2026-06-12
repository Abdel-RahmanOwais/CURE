namespace CURE.Application.DTOs.Patients;

public class CreatePatientDto
{
    public long UserId { get; set; }

    public string BloodType { get; set; } = null!;

    public string EmergencyContact { get; set; } = null!;
}