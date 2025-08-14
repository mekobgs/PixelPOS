using MediatR;
using PixelPOS.Application.Roles.DTOs;

namespace PixelPOS.Application.Roles.Queries
{
    public class GetRoleByIdQuery : IRequest<RoleDto?>
    {
        public int Id { get; set; }
    }
}
