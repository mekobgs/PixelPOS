using Microsoft.EntityFrameworkCore;
using PixelPOS.Domain.Entities;
using PixelPOS.Domain.Repositories;
using PixelPOS.Infrastructure.Persistence;

namespace PixelPOS.Infrastructure.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly AppDbContext _context;

        public SubscriptionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Subscription?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Subscriptions
                .Include(s => s.Plan)
                .Include(s => s.Company)
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<List<Subscription>> GetByCompanyIdAsync(int companyId, CancellationToken cancellationToken = default)
        {
            return await _context.Subscriptions
                .Include(s => s.Plan)
                .Where(s => s.CompanyId == companyId)
                .OrderByDescending(s => s.StartDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<Subscription?> GetActiveByCompanyIdAsync(int companyId, CancellationToken cancellationToken = default)
        {
            return await _context.Subscriptions
                .Include(s => s.Plan)
                .FirstOrDefaultAsync(s => s.CompanyId == companyId && s.IsActive, cancellationToken);
        }

        public async Task AddAsync(Subscription subscription, CancellationToken cancellationToken = default)
        {
            await _context.Subscriptions.AddAsync(subscription, cancellationToken);
        }

        public Task UpdateAsync(Subscription subscription, CancellationToken cancellationToken = default)
        {
            _context.Subscriptions.Update(subscription);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
