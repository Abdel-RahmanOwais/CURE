namespace CURE.Application.DTOs.Users;

public class UserResponseDto
{
    public long Id { get; set; }

    public string FullName { get; set; }

    public string Email { get; set; }

    public string Gender { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }

    public string City { get; set; }


    // public bool IsActive { get; set; }

    public DateOnly? DateOfBirth { get; set; }
    public DateTime CreatedAt { get; set; }
}