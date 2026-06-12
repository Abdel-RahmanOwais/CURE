using CURE.Application.DTOs.Auth;
using CURE.Application.Exceptions;
using CURE.Application.Interfaces.Repositories;
using CURE.Application.Interfaces.Security;
using CURE.Application.Interfaces.Services;
using CURE.Domain.Entities;

namespace CURE.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHash _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public AuthService(IUserRepository userRepository, IPasswordHash passwordHasher, IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }

    public async Task<long> RegisterAsync(RegisterDto dto)
    {
        var existingUser = await _userRepository.GetByEmailAsync(dto.Email);

        if (existingUser != null)
        {
            throw new BadRequestException("Email already exists");
        }

        var patientRole = await _userRepository.GetRoleByNameAsync("Patient");

        if (patientRole == null)
        {
            throw new NotFoundException("Patient role not found");
        }

        var person = new Person
        {
            FullName = dto.FullName,
            Gender = dto.Gender,
            PhoneNumber = dto.PhoneNumber,
            City = dto.City,
            DateOfBirth = dto.DateOfBirth,
            CreatedAt = DateTime.UtcNow,
            Address = dto.Address,

        };

        var patient = new Patient
        {
            BloodType = dto.BloodType,
            EmergencyContact = dto.EmergencyContact,
        };

        var user = new Users
        {
            Email = dto.Email,
            PasswordHash = _passwordHasher.Hash(dto.Password),
            IsActive = true,
            CreatedAt = DateTime.UtcNow,


            Person = person,
            Patient = patient

        };

        user.Roles.Add(patientRole);

        await _userRepository.AddAsync(user);

        await _userRepository.SaveChangesAsync();

        return user.Id;
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email);

        if (user == null)
        {
            throw new BadRequestException("Invalid email or password");
        }

        bool isValidPassword = _passwordHasher.Verify(dto.Password, user.PasswordHash);

        if (!isValidPassword)
        {
            throw new BadRequestException("Invalid email or password");
        }

        var token = _jwtProvider.GenerateToken(user);

        return new AuthResponseDto
        {
            Token = token,
            ExpireAt = DateTime.UtcNow.AddMinutes(60)
        };
    }
}