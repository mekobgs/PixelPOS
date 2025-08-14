using MediatR;

namespace PixelPOS.Application.Plans.Commands;

public class DeletePlanCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
