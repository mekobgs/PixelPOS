using MediatR;
using PixelPOS.Application.Users.DTOs;
using PixelPOS.Application.Users.Queries;
using PixelPOS.Domain.Repositories;

namespace PixelPOS.Application.Users.Handlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

            if (user == null || !user.IsActive)
                return null;

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
                LastLogin = user.LastLogin,
                Roles = user.UserRoles.Select(ur => ur.Role?.Name ?? string.Empty).ToList()
            };
        }
    }
}
