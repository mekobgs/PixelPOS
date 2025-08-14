using System;

namespace PixelPOS.Domain.Entities
{
    public class PasswordResetToken
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public bool Used { get; set; } = false;

        public User? User { get; set; }
    }
}
