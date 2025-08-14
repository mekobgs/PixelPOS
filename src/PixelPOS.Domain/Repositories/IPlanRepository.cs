using PixelPOS.Domain.Entities;

namespace PixelPOS.Domain.Repositories;

public interface IPlanRepository
{
    Task<bool> ExistsByNameAsync(string name, int? excludeId = null, CancellationToken cancellationToken = default);
    Task<Plan?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<List<Plan>> GetPagedAsync(int page, int pageSize, string? search, CancellationToken cancellationToken = default);
    Task<int> GetTotalCountAsync(string? search, CancellationToken cancellationToken = default);
    Task AddAsync(Plan plan, CancellationToken cancellationToken = default);
    Task UpdateAsync(Plan plan, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<List<Plan>> GetAllAsync(CancellationToken cancellationToken = default);
}
