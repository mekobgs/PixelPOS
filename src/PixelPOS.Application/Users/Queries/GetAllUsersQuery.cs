using MediatR;
using PixelPOS.Application.Users.DTOs;

namespace PixelPOS.Application.Users.Queries
{
    public class GetAllUsersQuery : IRequest<PagedUsersDto>
    {
        public int CompanyId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string? Search { get; set; }
    }
}
