using AutoMapper;
using CURE.Application.DTOs.Nurses;
using CURE.Application.Exceptions;
using CURE.Application.Interfaces.Repositories;
using CURE.Application.Interfaces.Security;
using CURE.Application.Interfaces.Services;
using CURE.Domain.Entities;

namespace CURE.Application.Services;

public class NurseService : INurseService
{
    private readonly INurseRepository _nurseRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHash _passwordHasher;

    public NurseService(
        INurseRepository nurseRepository,
        IUserRepository userRepository,
        IMapper mapper,
        IPasswordHash passwordHasher)
    {
        _nurseRepository = nurseRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    public async Task<List<NurseResponseDto>> GetAllAsync(
        int pageNumber,
        int pageSize)
    {
        var nurses =
            await _nurseRepository
                .GetAllAsync(pageNumber, pageSize);

        return _mapper.Map<List<NurseResponseDto>>(nurses);
    }

    public async Task<NurseResponseDto?> GetByIdAsync(long id)
    {
        var nurse =
            await _nurseRepository.GetByIdAsync(id);

        if (nurse is null)
            throw new NotFoundException(
                "Nurse Not Found");

        return _mapper.Map<NurseResponseDto>(nurse);
    }

    public async Task<NurseResponseDto?> GetByUserIdAsync(long userId)
    {
        var nurse =
            await _nurseRepository.GetByUserIdAsync(userId);

        if (nurse is null)
            throw new NotFoundException(
                "Nurse Not Found");

        return _mapper.Map<NurseResponseDto>(nurse);
    }

    public async Task<long> CreateAsync(CreateNurseDto dto)
    {
        var existingUser =
            await _userRepository
                .GetByEmailAsync(dto.Email);

        if (existingUser != null)
        {
            throw new BadRequestException(
                "Email already exists");
        }

        var nurseRole =
            await _userRepository
                .GetRoleByNameAsync("Nurse");

        if (nurseRole is null)
        {
            throw new NotFoundException("Nurse role not found");
        }

        var person = new Person
        {
            FullName = dto.FullName,
            DateOfBirth = dto.DateOfBirth,
            Gender = dto.Gender,
            PhoneNumber = dto.PhoneNumber,
            Address = dto.Address,
            City = dto.City,
            CreatedAt = DateTime.UtcNow
        };

        var nurse = new Nurse
        {
            Specialization = dto.Specialization,
            LicenseNumber = dto.LicenseNumber,
            YearsOfExperience = dto.YearsOfExperience,
            CreatedAt = DateTime.UtcNow
        };

        var user = new Users
        {
            Email = dto.Email,
            PasswordHash =
                _passwordHasher.Hash(dto.Password),

            IsActive = true,
            CreatedAt = DateTime.UtcNow,

            Person = person,
            Nurse = nurse
        };

        user.Roles.Add(nurseRole);

        await _userRepository.AddAsync(user);

        await _userRepository.SaveChangesAsync();

        return user.Id;
    }
    public async Task UpdateAsync(
        long id,
        UpdateNurseDto dto)
    {
        var nurse =
            await _nurseRepository.GetByIdAsync(id);

        if (nurse is null)
            throw new NotFoundException(
                "Nurse not found");

        _mapper.Map(dto, nurse);

        nurse.UpdatedAt = DateTime.UtcNow;

        _nurseRepository.Update(nurse);

        await _nurseRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var nurse =
            await _nurseRepository.GetByIdAsync(id);

        if (nurse is null)
            throw new NotFoundException(
                "Nurse not found");

        nurse.IsDeleted = true;

        nurse.DeletedAt = DateTime.UtcNow;

        _nurseRepository.Update(nurse);

        await _nurseRepository.SaveChangesAsync();
    }
}