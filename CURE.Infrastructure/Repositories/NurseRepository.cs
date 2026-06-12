using CURE.Application.Interfaces.Repositories;
using CURE.Domain.Entities;
using CURE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CURE.Infrastructure.Repositories;

public class NurseRepository : INurseRepository
{
    private readonly AppDBContext _context;

    public NurseRepository(AppDBContext context)
    {
        _context = context;
    }

    public async Task<List<Nurse>> GetAllAsync(int pageNumber, int pageSize)
    {
        return await _context.Nurses
            .AsNoTracking()

            .Include(x => x.User)
            .ThenInclude(x => x.Person)

            .Where(x => !x.IsDeleted)

            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)

            .ToListAsync();
    }

    public async Task<Nurse?> GetByIdAsync(long id)
    {
        return await _context.Nurses
            .AsNoTracking()

            .Include(x => x.User)
            .ThenInclude(x => x.Person)

            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                !x.IsDeleted);
    }

    public async Task<Nurse?> GetByUserIdAsync(long userId)
    {
        return await _context.Nurses
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
                x.UserId == userId &&
                !x.IsDeleted);
    }

    public async Task AddAsync(Nurse nurse)
    {
        await _context.Nurses.AddAsync(nurse);
    }

    public void Update(Nurse nurse)
    {
        _context.Nurses.Update(nurse);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}