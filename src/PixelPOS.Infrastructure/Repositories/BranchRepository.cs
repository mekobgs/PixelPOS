using Microsoft.EntityFrameworkCore;
using PixelPOS.Domain.Entities;
using PixelPOS.Domain.Repositories;
using PixelPOS.Infrastructure.Persistence;

namespace PixelPOS.Infrastructure.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly AppDbContext _context;

        public BranchRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsByNameAsync(int companyId, string name, int? excludeId = null, CancellationToken cancellationToken = default)
        {
            return await _context.Branches.AnyAsync(
                b => b.CompanyId == companyId && b.Name == name && (!excludeId.HasValue || b.Id != excludeId.Value),
                cancellationToken);
        }

        public async Task<Branch?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Branches
                .Include(b => b.Company)
                .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
        }

        public async Task<List<Branch>> GetByCompanyIdAsync(int companyId, CancellationToken cancellationToken = default)
        {
            return await _context.Branches
                .Where(b => b.CompanyId == companyId)
                .OrderBy(b => b.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Branch>> GetPagedAsync(int companyId, int page, int pageSize, string? search, CancellationToken cancellationToken = default)
        {
            var query = _context.Branches.Where(b => b.CompanyId == companyId);
            if (!string.IsNullOrEmpty(search))
                query = query.Where(b => b.Name.Contains(search));
            return await query
                .OrderBy(b => b.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> GetTotalCountAsync(int companyId, string? search, CancellationToken cancellationToken = default)
        {
            var query = _context.Branches.Where(b => b.CompanyId == companyId);
            if (!string.IsNullOrEmpty(search))
                query = query.Where(b => b.Name.Contains(search));
            return await query.CountAsync(cancellationToken);
        }

        public async Task AddAsync(Branch branch, CancellationToken cancellationToken = default)
        {
            await _context.Branches.AddAsync(branch, cancellationToken);
        }

        public Task UpdateAsync(Branch branch, CancellationToken cancellationToken = default)
        {
            _context.Branches.Update(branch);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
