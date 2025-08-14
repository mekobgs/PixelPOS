using PixelPOS.Domain.Entities;
using PixelPOS.Domain.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using System.Security.Cryptography;

namespace PixelPOS.Infrastructure.Security
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<string> GenerateJwtToken(User user)
        {
            var jwtKey = _configuration["Jwt:Key"];
            var jwtIssuer = _configuration["Jwt:Issuer"];
            var jwtAudience = _configuration["Jwt:Audience"];
            var jwtExpireMinutes = int.Parse(_configuration["Jwt:ExpireMinutes"] ?? "30");

            var roles = user.UserRoles.Select(ur => ur.Role?.Name ?? "").ToArray();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim("companyId", user.CompanyId.ToString()),
                new Claim("branchId", user.BranchId?.ToString() ?? ""),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email)
            };
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwtExpireMinutes),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return Task.FromResult(tokenString);
        }

        public Task<string> GenerateRefreshToken()
        {
            var randomBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return Task.FromResult(Convert.ToBase64String(randomBytes));
        }

        public Task<bool> ValidateJwtToken(string token, out int userId, out int companyId, out string[] roles)
        {
            userId = 0;
            companyId = 0;
            roles = Array.Empty<string>();

            var jwtKey = _configuration["Jwt:Key"];
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var sub = principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
                var companyIdClaim = principal.FindFirst("companyId")?.Value;
                var roleClaims = principal.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToArray();

                if (!int.TryParse(sub, out userId) || !int.TryParse(companyIdClaim, out companyId))
                    return Task.FromResult(false);

                roles = roleClaims;
                return Task.FromResult(true);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }
    }
}
