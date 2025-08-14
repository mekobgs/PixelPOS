using MediatR;
using PixelPOS.Application.Companies.DTOs;

namespace PixelPOS.Application.Companies.Commands
{
    public class CreateCompanyCommand : IRequest<CompanyDto>
    {
        public string Name { get; set; } = string.Empty;
        public string? TaxId { get; set; }
        public string? Address { get; set; }
        public string? LogoUrl { get; set; }
        public int PlanId { get; set; }
        public string? PaymentStatus { get; set; }
    }
}
