using AutoMapper;
using CURE.Application.DTOs.Patients;
using CURE.Application.Exceptions;
using CURE.Application.Interfaces.Repositories;
using CURE.Application.Interfaces.Services;
using CURE.Domain.Entities;

namespace CURE.Application.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public PatientService(
        IPatientRepository patientRepository,
        IUserRepository userRepository,
        IMapper mapper)
    {
        _patientRepository = patientRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<PatientResponseDto>> GetAllAsync(
        int pageNumber,
        int pageSize)
    {
        var patients =
            await _patientRepository
                .GetAllAsync(pageNumber, pageSize);

        return _mapper.Map<List<PatientResponseDto>>(patients);
    }

    public async Task<PatientResponseDto?> GetByIdAsync(long id)
    {
        var patient =
            await _patientRepository.GetByIdAsync(id);

        if (patient is null)
            throw new NotFoundException(
                "Patient not found");

        return _mapper.Map<PatientResponseDto>(patient);
    }

    public async Task<long> CreateAsync(
        CreatePatientDto dto)
    {
        var user =
            await _userRepository
                .GetByIdAsync(dto.UserId);

        if (user is null)
            throw new BadRequestException(
                "User not found");

        var patient =
            _mapper.Map<Patient>(dto);

        patient.CreatedAt = DateTime.UtcNow;

        await _patientRepository
            .AddAsync(patient);

        await _patientRepository
            .SaveChangesAsync();

        return patient.Id;
    }

    public async Task UpdateAsync(
        long id,
        UpdatePatientDto dto)
    {
        var patient =
            await _patientRepository
                .GetByIdAsync(id);

        if (patient is null)
            throw new NotFoundException(
                "Patient not found");

        _mapper.Map(dto, patient);

        patient.UpdatedAt =
            DateTime.UtcNow;

        _patientRepository.Update(patient);

        await _patientRepository
            .SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var patient =
            await _patientRepository
                .GetByIdAsync(id);

        if (patient is null)
            throw new NotFoundException(
                "Patient not found");

        patient.IsDeleted = true;
        patient.DeletedAt = DateTime.UtcNow;

        _patientRepository.Update(patient);

        await _patientRepository
            .SaveChangesAsync();
    }
}