using MediatR;
using PixelPOS.Application.Roles.Commands;
using PixelPOS.Domain.Repositories;


namespace PixelPOS.Application.Roles.Handlers
{
    public class DeleteRoleHandler : IRequestHandler<DeleteRoleCommand, Unit>
    {
        private readonly IRoleRepository _roleRepository;

        public DeleteRoleHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetByIdAsync(request.Id, cancellationToken);
            if (role == null)
                throw new System.Exception("Role not found.");

            // Soft delete: in a real scenario, set IsActive = false, here we'll just not implement full deletion logic for simplicity
            // _roleRepository.Update(role, cancellationToken); // if you have an IsActive field
            // For now, consider you handle deletion at business logic level

            return Unit.Value;
        }
    }
}
