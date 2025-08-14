using MediatR;
using System.Collections.Generic;
using PixelPOS.Application.Roles.DTOs;

namespace PixelPOS.Application.Roles.Queries
{
    public class GetAllRolesQuery : IRequest<List<RoleDto>> { }
}
