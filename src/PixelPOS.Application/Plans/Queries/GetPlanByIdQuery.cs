using MediatR;
using PixelPOS.Application.Plans.DTOs;

namespace PixelPOS.Application.Plans.Queries;

public class GetPlanByIdQuery : IRequest<PlanDto?>
{
    public int Id { get; set; }
}
