using MediatR;
using PixelPOS.Application.Branches.DTOs;

namespace PixelPOS.Application.Branches.Commands
{
    public class CreateBranchCommand : IRequest<BranchDto>
    {
        public int CompanyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? PrinterName { get; set; }
        public string? OpeningHours { get; set; }
    }
}
