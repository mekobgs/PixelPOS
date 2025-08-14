using MediatR;
using PixelPOS.Application.Auth.Commands;
using PixelPOS.Application.Auth.DTOs;
using PixelPOS.Domain.Repositories;
using PixelPOS.Domain.Security;

namespace PixelPOS.Application.Auth.Handlers
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, LoginResponseDto>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public RefreshTokenHandler(IRefreshTokenRepository refreshTokenRepository, IUserRepository userRepository, ITokenService tokenService)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<LoginResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var stored = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken, cancellationToken);
            if (stored == null || stored.IsRevoked || stored.ExpiryDate < DateTime.UtcNow)
                throw new Exception("Invalid or expired refresh token.");

            var user = await _userRepository.GetByIdAsync(stored.UserId, cancellationToken);
            if (user == null)
                throw new Exception("User not found.");

            var jwtToken = await _tokenService.GenerateJwtToken(user);
            var newRefreshToken = await _tokenService.GenerateRefreshToken();

            stored.IsRevoked = true;
            await _refreshTokenRepository.RevokeAsync(request.RefreshToken, cancellationToken);
            await _refreshTokenRepository.AddAsync(new PixelPOS.Domain.Entities.RefreshToken
            {
                UserId = user.Id,
                Token = newRefreshToken,
                ExpiryDate = DateTime.UtcNow.AddDays(7)
            }, cancellationToken);
            await _refreshTokenRepository.SaveChangesAsync(cancellationToken);

            return new LoginResponseDto
            {
                JwtToken = jwtToken,
                RefreshToken = newRefreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(60),
                UserId = user.Id,
                CompanyId = user.CompanyId,
                BranchId = user.BranchId,
                Roles = user.UserRoles.Select(ur => ur.Role?.Name ?? string.Empty).ToList()
            };
        }
    }
}
