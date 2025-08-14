using MediatR;
using PixelPOS.Application.Users.Commands;
using PixelPOS.Application.Users.DTOs;
using PixelPOS.Domain.Repositories;

namespace PixelPOS.Application.Users.Handlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UpdateUserHandler(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

            if (user == null)
                throw new Exception("User not found.");

            if (await _userRepository.ExistsByUsernameAsync(request.CompanyId, request.Username, request.Id, cancellationToken))
                throw new Exception("A user with the same username already exists in this company.");

            user.Username = request.Username;
            user.Email = request.Email;
            user.FullName = request.FullName;
            user.BranchId = request.BranchId;
            user.IsActive = request.IsActive;

            // Update roles
            var newRoleIds = request.RoleIds.Distinct().ToList();
            var roles = new List<string>();
            user.UserRoles.Clear();

            foreach (var roleId in newRoleIds)
            {
                var role = await _roleRepository.GetByIdAsync(roleId, cancellationToken);
                if (role == null)
                    throw new Exception($"Role with id {roleId} not found.");
                user.UserRoles.Add(new Domain.Entities.UserRole { UserId = user.Id, RoleId = role.Id });
                roles.Add(role.Name);
            }

            await _userRepository.UpdateAsync(user, cancellationToken);
            await _userRepository.SaveChangesAsync(cancellationToken);

            return new UserDto
            {
                Id = user.Id,
                CompanyId = user.CompanyId,
                BranchId = user.BranchId,
                Username = user.Username,
                Email = user.Email,
                FullName = user.FullName,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                Roles = roles
            };
        }
    }
}
