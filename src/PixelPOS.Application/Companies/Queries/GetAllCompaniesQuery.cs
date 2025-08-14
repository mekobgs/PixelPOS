using MediatR;
using PixelPOS.Application.Companies.DTOs;

namespace PixelPOS.Application.Companies.Queries
{
    public class GetAllCompaniesQuery : IRequest<PagedCompaniesDto>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string? Search { get; set; }
    }

}
