using CURE.Application.Interfaces.Repositories.IBooking;
using CURE.Domain.Entities;
using CURE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CURE.Infrastructure.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly AppDBContext _context;

    public BookingRepository(AppDBContext context)
    {
        _context = context;
    }

    public async Task<List<Booking>> GetAllAsync(int pageNumber, int pageSize)
    {
        return await _context.Bookings
            .Include(x => x.Patient)
                .ThenInclude(x => x.User)
                    .ThenInclude(x => x.Person)

            .Include(x => x.Nurse)
                .ThenInclude(x => x.User)
                    .ThenInclude(x => x.Person)

            .Include(x => x.BookingStatus)

            .Where(x => !x.IsDeleted)

            .OrderByDescending(x => x.CreatedAt)

            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)

            .ToListAsync();
    }

    public async Task<Booking?> GetByIdAsync(long id)
    {
        return await _context.Bookings
            .AsNoTracking()
            .Include(x => x.Patient)
                .ThenInclude(x => x.User)
                    .ThenInclude(x => x.Person)

            .Include(x => x.Nurse)
                .ThenInclude(x => x.User)
                    .ThenInclude(x => x.Person)

            .Include(x => x.BookingStatus)

            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
    }

    public async Task AddAsync(Booking booking)
    {
        await _context.Bookings.AddAsync(booking);
    }

    public void Update(Booking booking)
    {
        _context.Bookings.Update(booking);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}