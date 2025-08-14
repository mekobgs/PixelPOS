using Microsoft.EntityFrameworkCore;
using PixelPOS.Domain.Entities;
using PixelPOS.Domain.Repositories;
using PixelPOS.Infrastructure.Persistence;

namespace PixelPOS.Infrastructure.Repositories
{
    public class PasswordResetTokenRepository : IPasswordResetTokenRepository
    {
        private readonly AppDbContext _context;

        public PasswordResetTokenRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PasswordResetToken resetToken, CancellationToken cancellationToken = default)
        {
            await _context.PasswordResetTokens.AddAsync(resetToken, cancellationToken);
        }

        public async Task<PasswordResetToken?> GetValidTokenAsync(int userId, string token, CancellationToken cancellationToken = default)
        {
            return await _context.PasswordResetTokens
                .Include(r => r.User)
                .FirstOrDefaultAsync(r =>
                    r.UserId == userId &&
                    r.Token == token &&
                    !r.Used &&
                    r.ExpiryDate > DateTime.UtcNow,
                    cancellationToken);
        }

        public Task MarkAsUsedAsync(PasswordResetToken resetToken, CancellationToken cancellationToken = default)
        {
            resetToken.Used = true;
            _context.PasswordResetTokens.Update(resetToken);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
