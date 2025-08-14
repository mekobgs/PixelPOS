using MediatR;
using PixelPOS.Application.Companies.DTOs;

namespace PixelPOS.Application.Companies.Commands
{
    public class UpdateCompanyCommand : IRequest<CompanyDto>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? TaxId { get; set; }
        public string? Address { get; set; }
        public string? LogoUrl { get; set; }
        public bool IsActive { get; set; }
    }
}
