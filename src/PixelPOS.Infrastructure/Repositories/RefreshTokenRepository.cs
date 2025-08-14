using Microsoft.EntityFrameworkCore;
using PixelPOS.Domain.Entities;
using PixelPOS.Domain.Repositories;
using PixelPOS.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace PixelPOS.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _context;

        public RefreshTokenRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
        {
            await _context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            return await _context.RefreshTokens
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt => rt.Token == token, cancellationToken);
        }

        public async Task RevokeAsync(string token, CancellationToken cancellationToken = default)
        {
            var rt = await _context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == token, cancellationToken);
            if (rt != null)
            {
                rt.IsRevoked = true;
                _context.RefreshTokens.Update(rt);
            }
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
