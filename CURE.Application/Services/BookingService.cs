using AutoMapper;
using CURE.Application.DTOs.Booking;
using CURE.Application.Exceptions;
using CURE.Application.Interfaces.Repositories;
using CURE.Application.Interfaces.Repositories.IBooking;
using CURE.Application.Interfaces.Security;
using CURE.Application.Interfaces.Services;
using CURE.Domain.Entities;
using CURE.Domain.Enums;

namespace CURE.Application.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;

    private readonly IPatientRepository _patientRepository;

    private readonly INurseRepository _nurseRepository;

    private readonly IBookingStatusHistoryRepository _historyRepository;

    private readonly IMapper _mapper;

    private readonly ICurrentUserService _currentUser;

    public BookingService(IBookingRepository bookingRepository, IPatientRepository patientRepository, INurseRepository nurseRepository,
    IBookingStatusHistoryRepository historyRepository, IMapper mapper, ICurrentUserService currentUserService)
    {
        _bookingRepository = bookingRepository;

        _patientRepository = patientRepository;

        _nurseRepository = nurseRepository;

        _historyRepository = historyRepository;

        _mapper = mapper;

        _currentUser = currentUserService;
    }

    public async Task<List<BookingResponseDto>> GetAllAsync(int pageNumber, int pageSize)
    {

        var bookings = await _bookingRepository.GetAllAsync(pageNumber, pageSize);

        return _mapper.Map<List<BookingResponseDto>>(bookings);
    }

    public async Task<BookingResponseDto?> GetByIdAsync(long id)
    {


        var booking = await _bookingRepository.GetByIdAsync(id);

        if (booking is null)
            throw new NotFoundException(
                $"Booking with id {id} not found");


        if (_currentUser.IsPatient)
        {
            if (booking.Patient.UserId != _currentUser.UserId)
            {
                throw new ForbiddenException(
                    "You can only access your own bookings");
            }
        }

        if (_currentUser.IsNurse)
        {
            if (booking.Nurse.UserId != _currentUser.UserId)
            {
                throw new ForbiddenException(
                    "You can only access your own bookings");
            }
        }
        return _mapper.Map<BookingResponseDto>(booking);
    }

    public async Task<long> CreateAsync(CreateBookingDto dto)
    {
        if (_currentUser.IsNurse)
        {
            throw new ForbiddenException("Only patients can create bookings");
        }
        var patient =
            await _patientRepository.GetByUserIdAsync(_currentUser.UserId);

        if (patient is null)
            throw new NotFoundException($"Patient profile not found");

        var nurse = await _nurseRepository.GetByIdAsync(dto.NurseId);

        if (nurse is null)
            throw new NotFoundException($"Nurse with id {dto.NurseId} not found");

        if (dto.AppointmentDate <= DateTime.Now)
            throw new BadRequestException(
                "Appointment date must be in the future");

        var booking = new Booking
        {
            PatientId = patient.Id,

            NurseId = dto.NurseId,

            AppointmentDate =
                dto.AppointmentDate,

            BookingStatusId =
                (int)BookingStatusEnum.Pending,

            CreatedAt = DateTime.UtcNow
        };

        await _bookingRepository.AddAsync(
            booking);

        await _bookingRepository.SaveChangesAsync();

        return booking.Id;
    }

    public async Task UpdateAsync(long id, UpdateBookingDto dto)
    {
        if (!_currentUser.IsNurse)
        {
            throw new ForbiddenException("Only patients can update bookings");
        }

        var booking = await _bookingRepository.GetByIdAsync(id);

        if (booking is null)
            throw new NotFoundException($"Booking with id {id} not found");

        booking.AppointmentDate = dto.AppointmentDate;

        booking.UpdatedAt = DateTime.UtcNow;

        _bookingRepository.Update(booking);

        await _bookingRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {

        var booking = await _bookingRepository.GetByIdAsync(id);

        if (booking is null)
            throw new NotFoundException($"Booking with id {id} not found");

        booking.IsDeleted = true;

        booking.DeletedAt = DateTime.UtcNow;

        _bookingRepository.Update(booking);

        await _bookingRepository.SaveChangesAsync();
    }

    public async Task ChangeStatusAsync(long bookingId, int statusId, long changedByUserId)
    {
        if (_currentUser.IsPatient)
        {
            throw new ForbiddenException("Only nurses can change booking status");
        }
        var booking = await _bookingRepository.GetByIdAsync(bookingId);

        if (booking is null)
            throw new NotFoundException("Booking not found");

        var oldStatus =
            booking.BookingStatusId;

        booking.BookingStatusId = statusId;

        booking.UpdatedAt = DateTime.UtcNow;

        _bookingRepository.Update(booking);

        var history = new BookingStatusHistory
        {
            BookingId = booking.Id,

            OldStatusId = oldStatus,

            NewStatusId = statusId,

            ChangedByUserId = changedByUserId,

            ChangedAt = DateTime.UtcNow
        };

        await _historyRepository.AddAsync(history);

        await _bookingRepository.SaveChangesAsync();
    }

    public async Task<List<BookingHistoryDto>> GetHistoryAsync(long bookingId)
    {
        if (_currentUser.IsPatient)
        {
            throw new ForbiddenException("Only nurses can view booking history");
        }

        var history = await _historyRepository.GetBookingHistoryAsync(bookingId);

        return history.Select(x =>
            new BookingHistoryDto
            {
                OldStatus = x.OldStatus?.Name,

                NewStatus = x.NewStatus.Name,

                ChangedBy = x.ChangedByUser
                            .Person
                            .FullName,

                ChangedAt = x.ChangedAt
            })
            .ToList();
    }
}