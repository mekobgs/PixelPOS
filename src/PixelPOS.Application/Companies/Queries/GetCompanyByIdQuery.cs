using MediatR;
using PixelPOS.Application.Companies.DTOs;

namespace PixelPOS.Application.Companies.Queries
{
    public class GetCompanyByIdQuery : IRequest<CompanyDto?>
    {
        public int Id { get; set; }
    }
}
