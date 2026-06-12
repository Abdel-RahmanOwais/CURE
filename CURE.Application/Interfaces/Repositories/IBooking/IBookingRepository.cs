using CURE.Domain.Entities;

namespace CURE.Application.Interfaces.Repositories.IBooking
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetAllAsync(
            int pageNumber,
            int pageSize);

        Task<Booking?> GetByIdAsync(long id);

        Task AddAsync(Booking booking);

        void Update(Booking booking);

        Task SaveChangesAsync();
    }
}
