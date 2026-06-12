using CURE.Application.DTOs.Booking;

namespace CURE.Application.Interfaces.Services
{
    public interface IBookingService
    {
        Task<List<BookingResponseDto>> GetAllAsync(int pageNumber, int pageSize);

        Task<BookingResponseDto?> GetByIdAsync(long id);

        Task<long> CreateAsync(CreateBookingDto dto);

        Task UpdateAsync(long id, UpdateBookingDto dto);

        Task DeleteAsync(long id);

        Task ChangeStatusAsync(
            long bookingId,
            int statusId,
            long changedByUserId);

        Task<List<BookingHistoryDto>> GetHistoryAsync(long bookingId);
    }
}
