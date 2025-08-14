using MediatR;

namespace PixelPOS.Application.Auth.Commands
{
    public class ForgotPasswordCommand : IRequest<bool>
    {
        public string UsernameOrEmail { get; set; } = string.Empty;
    }
}
