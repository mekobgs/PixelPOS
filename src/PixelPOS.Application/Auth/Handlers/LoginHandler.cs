using MediatR;
using PixelPOS.Application.Auth.Commands;
using PixelPOS.Application.Auth.DTOs;
using PixelPOS.Domain.Security;
using PixelPOS.Domain.Repositories;

namespace PixelPOS.Application.Auth.Handlers
{
    public class LoginHandler : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public LoginHandler(IAuthService authService, ITokenService tokenService, IRefreshTokenRepository refreshTokenRepository)
        {
            _authService = authService;
            _tokenService = tokenService;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _authService.Authenticate(request.Username, request.Password);
            if (user == null)
                throw new Exception("Invalid credentials.");

            var jwtToken = await _tokenService.GenerateJwtToken(user);
            var refreshToken = await _tokenService.GenerateRefreshToken();

            var expires = DateTime.UtcNow.AddDays(7);
            await _refreshTokenRepository.AddAsync(new PixelPOS.Domain.Entities.RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken,
                ExpiryDate = expires
            }, cancellationToken);
            await _refreshTokenRepository.SaveChangesAsync(cancellationToken);

            return new LoginResponseDto
            {
                JwtToken = jwtToken,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(60),
                UserId = user.Id,
                CompanyId = user.CompanyId,
                BranchId = user.BranchId,
                Roles = user.UserRoles.Select(ur => ur.Role?.Name ?? string.Empty).ToList()
            };
        }
    }
}
