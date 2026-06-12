using CURE.Application.Interfaces.Repositories;
using CURE.Domain.Entities;
using CURE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CURE.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDBContext _context;
        public UserRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Users user)
        {
            await _context.Users.AddAsync(user);
        }

        public void SoftDelete(Users user)
        {
            user.IsDeleted = true;
            user.DeletedAt = DateTime.UtcNow;
            _context.Users.Update(user);
        }

        public async Task<List<Users>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _context.Users.AsNoTracking()
                .Include(u => u.Person)
                .Where(d => !d.IsDeleted)
                .OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Users?> GetByIdAsync(long id, bool trackChanges = false)
        {
            var query = _context.Users.AsQueryable();

            if (!trackChanges)
            {
                query = query.AsNoTracking();
            }

            return await query.Include(u => u.Person)
                        .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
        }

        public async Task<Users?> GetByEmailAsync(string email, bool trackChanges = false)
        {
            var query = _context.Users.AsQueryable();

            if (!trackChanges)
            {
                query = query.AsNoTracking();
            }

            return await query.Include(u => u.Person)
                        .Include(u => u.Roles)
                        .FirstOrDefaultAsync(u => u.Email == email && !u.IsDeleted);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Users user)
        {
            _context.Users.Update(user);
        }

        public async Task<Role?> GetRoleByNameAsync(string roleName)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
        }
    }
}
