using PixelPOS.Domain.Entities;

namespace PixelPOS.Domain.Security
{
    public interface IAuthService
    {
        Task<User?> Authenticate(string username, string password);
        Task<string> GeneratePasswordResetToken(User user);
        Task<bool> ResetPassword(int userId, string newPassword, string resetToken);
        Task<bool> ChangePassword(int userId, string oldPassword, string newPassword);
    }
}
