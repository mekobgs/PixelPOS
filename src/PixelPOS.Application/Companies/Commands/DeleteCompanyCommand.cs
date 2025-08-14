using MediatR;

namespace PixelPOS.Application.Companies.Commands
{
    public class DeleteCompanyCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
