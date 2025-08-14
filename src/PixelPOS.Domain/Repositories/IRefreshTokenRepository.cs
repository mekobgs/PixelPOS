using System.Threading;
using System.Threading.Tasks;
using PixelPOS.Domain.Entities;

namespace PixelPOS.Domain.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);
        Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        Task RevokeAsync(string token, CancellationToken cancellationToken = default);
    }
}
