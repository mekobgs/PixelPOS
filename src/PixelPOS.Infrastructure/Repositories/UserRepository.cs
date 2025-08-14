using Microsoft.EntityFrameworkCore;
using PixelPOS.Domain.Entities;
using PixelPOS.Domain.Repositories;
using PixelPOS.Infrastructure.Persistence;

namespace PixelPOS.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsByUsernameAsync(int companyId, string username, int? excludeId = null, CancellationToken cancellationToken = default)
        {
            return await _context.Users.AnyAsync(
                u => u.CompanyId == companyId && u.Username == username && (!excludeId.HasValue || u.Id != excludeId.Value),
                cancellationToken);
        }

        public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Users
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            return await _context.Users
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Username == username, cancellationToken);
        }

        public async Task<List<User>> GetPagedAsync(int companyId, int page, int pageSize, string? search, CancellationToken cancellationToken = default)
        {
            var query = _context.Users.Where(u => u.CompanyId == companyId);
            if (!string.IsNullOrEmpty(search))
                query = query.Where(u => u.Username.Contains(search) || (u.FullName != null && u.FullName.Contains(search)));
            return await query
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .OrderBy(u => u.Username)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> GetTotalCountAsync(int companyId, string? search, CancellationToken cancellationToken = default)
        {
            var query = _context.Users.Where(u => u.CompanyId == companyId);
            if (!string.IsNullOrEmpty(search))
                query = query.Where(u => u.Username.Contains(search) || (u.FullName != null && u.FullName.Contains(search)));
            return await query.CountAsync(cancellationToken);
        }

        public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        {
            await _context.Users.AddAsync(user, cancellationToken);
        }

        public Task UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            _context.Users.Update(user);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
