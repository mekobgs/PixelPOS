using MediatR;
using PixelPOS.Application.Auth.DTOs;

namespace PixelPOS.Application.Auth.Commands
{
    public class LoginCommand : IRequest<LoginResponseDto>
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
