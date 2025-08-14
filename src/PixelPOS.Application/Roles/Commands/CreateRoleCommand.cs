using MediatR;
using PixelPOS.Application.Roles.DTOs;

namespace PixelPOS.Application.Roles.Commands
{
    public class CreateRoleCommand : IRequest<RoleDto>
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
