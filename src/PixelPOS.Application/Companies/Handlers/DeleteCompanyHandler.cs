using MediatR;
using PixelPOS.Application.Companies.Commands;
using PixelPOS.Domain.Repositories;

namespace PixelPOS.Application.Companies.Handlers
{
    public class DeleteCompanyHandler : IRequestHandler<DeleteCompanyCommand, Unit>
    {
        private readonly ICompanyRepository _companyRepository;

        public DeleteCompanyHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<Unit> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(request.Id, cancellationToken);

            if (company == null)
                throw new System.Exception("Company not found.");

            company.IsActive = false;

            await _companyRepository.UpdateAsync(company, cancellationToken);
            await _companyRepository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
