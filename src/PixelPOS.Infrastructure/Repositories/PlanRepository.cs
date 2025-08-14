using Microsoft.EntityFrameworkCore;
using PixelPOS.Domain.Entities;
using PixelPOS.Domain.Repositories;
using PixelPOS.Infrastructure.Persistence;

namespace PixelPOS.Infrastructure.Repositories;

public class PlanRepository : IPlanRepository
{
    private readonly AppDbContext _context;

    public PlanRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsByNameAsync(string name, int? excludeId = null, CancellationToken cancellationToken = default)
    {
        return await _context.Plans.AnyAsync(
            p => p.Name == name && (!excludeId.HasValue || p.Id != excludeId.Value),
            cancellationToken);
    }

    public async Task<Plan?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Plans
            .Include(p => p.Subscriptions)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<List<Plan>> GetPagedAsync(int page, int pageSize, string? search, CancellationToken cancellationToken = default)
    {
        var query = _context.Plans.AsQueryable();
        if (!string.IsNullOrEmpty(search))
            query = query.Where(p => p.Name.Contains(search));
        return await query
            .OrderBy(p => p.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetTotalCountAsync(string? search, CancellationToken cancellationToken = default)
    {
        var query = _context.Plans.AsQueryable();
        if (!string.IsNullOrEmpty(search))
            query = query.Where(p => p.Name.Contains(search));
        return await query.CountAsync(cancellationToken);
    }

    public async Task AddAsync(Plan plan, CancellationToken cancellationToken = default)
    {
        await _context.Plans.AddAsync(plan, cancellationToken);
    }

    public Task UpdateAsync(Plan plan, CancellationToken cancellationToken = default)
    {
        _context.Plans.Update(plan);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Plan>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Plans.ToListAsync(cancellationToken);
    }
}
