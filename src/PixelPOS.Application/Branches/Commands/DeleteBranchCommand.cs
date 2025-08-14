using MediatR;

namespace PixelPOS.Application.Branches.Commands
{
    public class DeleteBranchCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
