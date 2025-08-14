using MediatR;

namespace PixelPOS.Application.Auth.Commands
{
    public class ResetPasswordCommand : IRequest<bool>
    {
        public int UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
