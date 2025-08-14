using PixelPOS.Domain.Entities;

namespace PixelPOS.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<bool> ExistsByUsernameAsync(int companyId, string username, int? excludeId = null, CancellationToken cancellationToken = default);
        Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
        Task<List<User>> GetPagedAsync(int companyId, int page, int pageSize, string? search, CancellationToken cancellationToken = default);
        Task<int> GetTotalCountAsync(int companyId, string? search, CancellationToken cancellationToken = default);
        Task AddAsync(User user, CancellationToken cancellationToken = default);
        Task UpdateAsync(User user, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
