using PixelPOS.Domain.Entities;

namespace PixelPOS.Domain.Repositories
{
    public interface ISubscriptionRepository
    {
        Task<Subscription?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Subscription>> GetByCompanyIdAsync(int companyId, CancellationToken cancellationToken = default);
        Task<Subscription?> GetActiveByCompanyIdAsync(int companyId, CancellationToken cancellationToken = default);
        Task AddAsync(Subscription subscription, CancellationToken cancellationToken = default);
        Task UpdateAsync(Subscription subscription, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
