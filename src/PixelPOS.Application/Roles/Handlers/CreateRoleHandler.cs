using MediatR;
using PixelPOS.Application.Roles.Commands;
using PixelPOS.Application.Roles.DTOs;
using PixelPOS.Domain.Entities;
using PixelPOS.Domain.Repositories;

namespace PixelPOS.Application.Roles.Handlers
{
    public class CreateRoleHandler : IRequestHandler<CreateRoleCommand, RoleDto>
    {
        private readonly IRoleRepository _roleRepository;

        public CreateRoleHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<RoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            if (await _roleRepository.GetByNameAsync(request.Name, cancellationToken) != null)
                throw new Exception("Role with the same name already exists.");

            var role = new Role
            {
                Name = request.Name,
                Description = request.Description
            };

            await _roleRepository.AddAsync(role, cancellationToken);
            await _roleRepository.SaveChangesAsync(cancellationToken);

            return new RoleDto
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            };
        }
    }
}
