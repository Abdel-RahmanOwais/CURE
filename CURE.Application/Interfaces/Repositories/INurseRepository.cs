using CURE.Domain.Entities;

namespace CURE.Application.Interfaces.Repositories;

public interface INurseRepository
{
    Task<List<Nurse>> GetAllAsync(
        int pageNumber,
        int pageSize);

    Task<Nurse?> GetByIdAsync(long id);
    Task<Nurse?> GetByUserIdAsync(long userId);
    Task AddAsync(Nurse nurse);

    void Update(Nurse nurse);

    Task SaveChangesAsync();
}