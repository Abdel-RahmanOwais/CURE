using CURE.Domain.Entities;

namespace CURE.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<List<Users>> GetAllAsync(int pageNumber, int pageSize);
        Task<Users?> GetByIdAsync(long id, bool trackChanges = false);
        Task<Users?> GetByEmailAsync(string email, bool trackChanges = false);
        Task AddAsync(Users user);

        void Update(Users user);

        void SoftDelete(Users user);
        Task<Role?> GetRoleByNameAsync(string roleName);
        Task SaveChangesAsync();
    }
}
