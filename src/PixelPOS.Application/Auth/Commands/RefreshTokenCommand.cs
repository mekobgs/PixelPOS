using MediatR;
using PixelPOS.Application.Auth.DTOs;

namespace PixelPOS.Application.Auth.Commands
{
    public class RefreshTokenCommand : IRequest<LoginResponseDto>
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}
