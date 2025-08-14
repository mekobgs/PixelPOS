using PixelPOS.Domain.Entities;

namespace PixelPOS.Domain.Repositories
{
    public interface IRoleRepository
    {
        Task<Role?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<List<Role>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Role role, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
