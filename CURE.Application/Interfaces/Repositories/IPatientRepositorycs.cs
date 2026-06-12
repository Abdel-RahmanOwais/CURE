using CURE.Domain.Entities;

namespace CURE.Application.Interfaces.Repositories;

public interface IPatientRepository
{
    Task<List<Patient>> GetAllAsync(
        int pageNumber,
        int pageSize);

    Task<Patient?> GetByIdAsync(long id);
    Task<Patient?> GetByUserIdAsync(long userId);

    Task AddAsync(Patient patient);

    void Update(Patient patient);

    Task SaveChangesAsync();
}