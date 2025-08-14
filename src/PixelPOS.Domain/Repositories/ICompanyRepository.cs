using PixelPOS.Domain.Entities;

namespace PixelPOS.Domain.Repositories
{
    public interface ICompanyRepository
    {
        Task<bool> ExistsByNameAsync(string name, int? excludeId = null, CancellationToken cancellationToken = default);
        Task<Company?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Company>> GetPagedAsync(int page, int pageSize, string? search, CancellationToken cancellationToken = default);
        Task<int> GetTotalCountAsync(string? search, CancellationToken cancellationToken = default);
        Task AddAsync(Company company, CancellationToken cancellationToken = default);
        Task UpdateAsync(Company company, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<List<Company>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
