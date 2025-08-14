using PixelPOS.Domain.Entities;

namespace PixelPOS.Domain.Security
{
    public interface ITokenService
    {
        Task<string> GenerateJwtToken(User user);
        Task<string> GenerateRefreshToken();
        Task<bool> ValidateJwtToken(string token, out int userId, out int companyId, out string[] roles);
    }
}
