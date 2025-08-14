using MediatR;
using PixelPOS.Application.Users.DTOs;

namespace PixelPOS.Application.Users.Commands
{
    public class CreateUserCommand : IRequest<UserDto>
    {
        public int CompanyId { get; set; }
        public int? BranchId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? FullName { get; set; }
        public string Password { get; set; } = string.Empty;
        public List<int> RoleIds { get; set; } = new();
    }
}
