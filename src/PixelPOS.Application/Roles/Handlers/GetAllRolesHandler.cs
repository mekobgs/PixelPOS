using MediatR;
using PixelPOS.Application.Roles.Queries;
using PixelPOS.Application.Roles.DTOs;
using PixelPOS.Domain.Repositories;


namespace PixelPOS.Application.Roles.Handlers
{
    public class GetAllRolesHandler : IRequestHandler<GetAllRolesQuery, List<RoleDto>>
    {
        private readonly IRoleRepository _roleRepository;

        public GetAllRolesHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<List<RoleDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleRepository.GetAllAsync(cancellationToken);
            return roles.Select(r => new RoleDto
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description
            }).ToList();
        }
    }
}
