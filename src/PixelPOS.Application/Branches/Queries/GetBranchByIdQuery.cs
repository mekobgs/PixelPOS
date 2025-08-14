using MediatR;
using PixelPOS.Application.Branches.DTOs;

namespace PixelPOS.Application.Branches.Queries
{
    public class GetBranchByIdQuery : IRequest<BranchDto?>
    {
        public int Id { get; set; }
    }
}
