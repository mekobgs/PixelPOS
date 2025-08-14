using MediatR;
using PixelPOS.Application.Plans.DTOs;

namespace PixelPOS.Application.Plans.Queries;

public class GetAllPlansQuery : IRequest<PagedPlansDto>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public string? Search { get; set; }
}
