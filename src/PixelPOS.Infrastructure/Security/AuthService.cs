using PixelPOS.Domain.Entities;
using PixelPOS.Domain.Repositories;
using PixelPOS.Domain.Security;
using System;
using System.Threading.Tasks;

namespace PixelPOS.Infrastructure.Security
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IPasswordResetTokenRepository _resetTokenRepository;

        public AuthService(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IPasswordResetTokenRepository resetTokenRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _resetTokenRepository = resetTokenRepository;
        }

        public async Task<User?> Authenticate(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null || !user.IsActive)
                return null;
            if (!_passwordHasher.Verify(password, user.PasswordHash))
                return null;
            return user;
        }

        public async Task<string> GeneratePasswordResetToken(User user)
        {
            var token = Guid.NewGuid().ToString("N");
            var expiry = DateTime.UtcNow.AddHours(2);

            var resetToken = new PasswordResetToken
            {
                UserId = user.Id,
                Token = token,
                ExpiryDate = expiry,
                Used = false
            };
            await _resetTokenRepository.AddAsync(resetToken);
            await _resetTokenRepository.SaveChangesAsync();

            // Aquí puedes enviar el token por correo/SMS
            return token;
        }

        public async Task<bool> ResetPassword(int userId, string newPassword, string resetToken)
        {
            var resetEntity = await _resetTokenRepository.GetValidTokenAsync(userId, resetToken);
            if (resetEntity == null || resetEntity.Used || resetEntity.ExpiryDate < DateTime.UtcNow)
                return false;

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return false;

            user.PasswordHash = _passwordHasher.Hash(newPassword);

            resetEntity.Used = true;
            await _resetTokenRepository.MarkAsUsedAsync(resetEntity);

            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();
            await _resetTokenRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangePassword(int userId, string oldPassword, string newPassword)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null || !_passwordHasher.Verify(oldPassword, user.PasswordHash))
                return false;

            user.PasswordHash = _passwordHasher.Hash(newPassword);
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();
            return true;
        }
    }
}
