using CURE.Domain.Entities;

namespace CURE.Application.Interfaces.Repositories.IBooking
{
    public interface IBookingStatusHistoryRepository
    {
        Task AddAsync(
            BookingStatusHistory history);

        Task<List<BookingStatusHistory>>
            GetBookingHistoryAsync(long bookingId);

        Task SaveChangesAsync();
    }
}
