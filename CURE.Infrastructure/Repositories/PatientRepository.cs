using CURE.Application.Interfaces.Repositories;
using CURE.Domain.Entities;
using CURE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CURE.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDBContext _context;

        public PatientRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<Patient>> GetAllAsync(
            int pageNumber,
            int pageSize)
        {
            return await _context.Patients
                .AsNoTracking()
                .Include(x => x.User)
                .ThenInclude(x => x.Person)

                .Where(x => !x.IsDeleted)

                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)

                .ToListAsync();
        }

        public async Task<Patient?> GetByIdAsync(long id)
        {
            return await _context.Patients
                .AsNoTracking()
                .Include(x => x.User)
                .ThenInclude(x => x.Person)

                .FirstOrDefaultAsync(
                    x => x.Id == id &&
                         !x.IsDeleted);
        }

        public async Task AddAsync(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
        }

        public void Update(Patient patient)
        {
            _context.Patients.Update(patient);
        }
        public async Task<Patient?> GetByUserIdAsync(long userId)
        {
            return await _context.Patients
                .FirstOrDefaultAsync(x =>
                    x.UserId == userId &&
                    !x.IsDeleted);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
