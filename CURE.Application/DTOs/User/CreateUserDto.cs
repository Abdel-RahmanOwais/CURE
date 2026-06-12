namespace CURE.Application.DTOs.Users;

public class CreateUserDto
{
    public string FullName { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string Gender { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string userRole { get; set; }
}