using Microsoft.EntityFrameworkCore;
using PixelPOS.Domain.Entities;
using PixelPOS.Domain.Repositories;
using PixelPOS.Infrastructure.Persistence;

namespace PixelPOS.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Role?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Roles
                .Include(r => r.UserRoles)
                .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        public async Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Roles
                .Include(r => r.UserRoles)
                .FirstOrDefaultAsync(r => r.Name == name, cancellationToken);
        }

        public async Task<List<Role>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Roles
                .Include(r => r.UserRoles)
                .OrderBy(r => r.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Role role, CancellationToken cancellationToken = default)
        {
            await _context.Roles.AddAsync(role, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
