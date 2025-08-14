using MediatR;
using PixelPOS.Application.Roles.Queries;
using PixelPOS.Application.Roles.DTOs;
using PixelPOS.Domain.Repositories;

namespace PixelPOS.Application.Roles.Handlers
{
    public class GetRoleByIdHandler : IRequestHandler<GetRoleByIdQuery, RoleDto?>
    {
        private readonly IRoleRepository _roleRepository;

        public GetRoleByIdHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<RoleDto?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetByIdAsync(request.Id, cancellationToken);
            if (role == null)
                return null;
            return new RoleDto
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            };
        }
    }
}
