using CURE.Application.Interfaces.Repositories.IBooking;
using CURE.Domain.Entities;
using CURE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CURE.Infrastructure.Repositories;

public class BookingStatusHistoryRepository : IBookingStatusHistoryRepository
{
    private readonly AppDBContext _context;

    public BookingStatusHistoryRepository(AppDBContext context)
    {
        _context = context;
    }

    public async Task AddAsync(BookingStatusHistory history)
    {
        await _context.BookingStatusHistories
            .AddAsync(history);
    }

    public async Task<List<BookingStatusHistory>> GetBookingHistoryAsync(long bookingId)
    {
        return await _context.BookingStatusHistories
            .AsNoTracking()
            .Include(x => x.OldStatus)
            .Include(x => x.NewStatus)

            .Include(x => x.ChangedByUser)
                .ThenInclude(x => x.Person)

            .Where(x => x.BookingId == bookingId)

            .OrderByDescending(x => x.ChangedAt)

            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}