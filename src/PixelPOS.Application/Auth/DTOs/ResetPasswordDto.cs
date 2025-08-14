namespace PixelPOS.Application.Auth.DTOs
{
    public class ResetPasswordDto
    {
        public int UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
