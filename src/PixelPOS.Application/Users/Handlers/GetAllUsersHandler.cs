using MediatR;
using PixelPOS.Application.Users.DTOs;
using PixelPOS.Application.Users.Queries;
using PixelPOS.Domain.Repositories;

namespace PixelPOS.Application.Users.Handlers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, PagedUsersDto>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<PagedUsersDto> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetPagedAsync(request.CompanyId, request.Page, request.PageSize, request.Search, cancellationToken);
            var total = await _userRepository.GetTotalCountAsync(request.CompanyId, request.Search, cancellationToken);

            var data = users.Select(user => new UserDto
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
            }).ToList();

            return new PagedUsersDto
            {
                Data = data,
                Page = request.Page,
                PageSize = request.PageSize,
                Total = total
            };
        }
    }
}
