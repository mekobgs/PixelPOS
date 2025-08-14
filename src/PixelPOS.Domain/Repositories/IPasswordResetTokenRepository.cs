using System.Threading;
using System.Threading.Tasks;
using PixelPOS.Domain.Entities;

namespace PixelPOS.Domain.Repositories
{
    public interface IPasswordResetTokenRepository
    {
        Task AddAsync(PasswordResetToken resetToken, CancellationToken cancellationToken = default);
        Task<PasswordResetToken?> GetValidTokenAsync(int userId, string token, CancellationToken cancellationToken = default);
        Task MarkAsUsedAsync(PasswordResetToken resetToken, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
