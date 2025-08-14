using MediatR;

namespace PixelPOS.Application.Auth.Commands
{
    public class ChangePasswordCommand : IRequest<bool>
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
