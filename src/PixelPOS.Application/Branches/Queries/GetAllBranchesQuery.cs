using MediatR;
using PixelPOS.Application.Branches.DTOs;

namespace PixelPOS.Application.Branches.Queries
{
    public class GetAllBranchesQuery : IRequest<PagedBranchesDto>
    {
        public int CompanyId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string? Search { get; set; }
    }
}
