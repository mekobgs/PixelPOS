using Microsoft.EntityFrameworkCore;
using PixelPOS.Domain.Entities;
using PixelPOS.Domain.Repositories;
using PixelPOS.Infrastructure.Persistence;

namespace PixelPOS.Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AppDbContext _context;

        public CompanyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsByNameAsync(string name, int? excludeId = null, CancellationToken cancellationToken = default)
        {
            return await _context.Companies.AnyAsync(
                c => c.Name == name && (!excludeId.HasValue || c.Id != excludeId.Value),
                cancellationToken);
        }

        public async Task<Company?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Companies
                .Include(c => (IEnumerable<Subscription>)c.Subscriptions!)
                .ThenInclude(s => s.Plan)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<List<Company>> GetPagedAsync(int page, int pageSize, string? search, CancellationToken cancellationToken = default)
        {
            var query = _context.Companies.AsQueryable();
            if (!string.IsNullOrEmpty(search))
                query = query.Where(c => c.Name.Contains(search));
            return await query
                .OrderBy(c => c.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(c => (IEnumerable<Subscription>)c.Subscriptions!)
                .ThenInclude(s => s.Plan)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> GetTotalCountAsync(string? search, CancellationToken cancellationToken = default)
        {
            var query = _context.Companies.AsQueryable();
            if (!string.IsNullOrEmpty(search))
                query = query.Where(c => c.Name.Contains(search));
            return await query.CountAsync(cancellationToken);
        }

        public async Task AddAsync(Company company, CancellationToken cancellationToken = default)
        {
            await _context.Companies.AddAsync(company, cancellationToken);
        }

        public Task UpdateAsync(Company company, CancellationToken cancellationToken = default)
        {
            _context.Companies.Update(company);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Company>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Companies
                .Include(c => (IEnumerable<Subscription>)c.Subscriptions!)
                .ThenInclude(s => s.Plan)
                .ToListAsync(cancellationToken);
        }
    }
}
