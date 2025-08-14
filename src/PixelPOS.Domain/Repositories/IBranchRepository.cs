using PixelPOS.Domain.Entities;

namespace PixelPOS.Domain.Repositories
{
    public interface IBranchRepository
    {
        Task<bool> ExistsByNameAsync(int companyId, string name, int? excludeId = null, CancellationToken cancellationToken = default);
        Task<Branch?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Branch>> GetByCompanyIdAsync(int companyId, CancellationToken cancellationToken = default);
        Task<List<Branch>> GetPagedAsync(int companyId, int page, int pageSize, string? search, CancellationToken cancellationToken = default);
        Task<int> GetTotalCountAsync(int companyId, string? search, CancellationToken cancellationToken = default);
        Task AddAsync(Branch branch, CancellationToken cancellationToken = default);
        Task UpdateAsync(Branch branch, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
