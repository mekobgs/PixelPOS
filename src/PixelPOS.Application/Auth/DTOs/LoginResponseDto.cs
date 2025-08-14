using System;
using System.Collections.Generic;

namespace PixelPOS.Application.Auth.DTOs
{
    public class LoginResponseDto
    {
        public string JwtToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public int? BranchId { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}
