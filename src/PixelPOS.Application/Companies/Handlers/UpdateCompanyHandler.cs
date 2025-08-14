using MediatR;
using PixelPOS.Application.Companies.Commands;
using PixelPOS.Application.Companies.DTOs;
using PixelPOS.Application.Subscriptions.DTOs;
using PixelPOS.Domain.Repositories;

namespace PixelPOS.Application.Companies.Handlers
{
    public class UpdateCompanyHandler : IRequestHandler<UpdateCompanyCommand, CompanyDto>
    {
        private readonly ICompanyRepository _companyRepository;

        public UpdateCompanyHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<CompanyDto> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(request.Id, cancellationToken);

            if (company == null)
                throw new System.Exception("Company not found.");

            if (await _companyRepository.ExistsByNameAsync(request.Name, request.Id, cancellationToken))
                throw new System.Exception("A company with the same name already exists.");

            company.Name = request.Name;
            company.TaxId = request.TaxId;
            company.Address = request.Address;
            company.LogoUrl = request.LogoUrl;
            company.IsActive = request.IsActive;

            await _companyRepository.UpdateAsync(company, cancellationToken);
            await _companyRepository.SaveChangesAsync(cancellationToken);

            var subscriptionsDto = company.Subscriptions?
                .Select(sub => new SubscriptionDto
                {
                    Id = sub.Id,
                    CompanyId = sub.CompanyId,
                    PlanId = sub.PlanId,
                    PlanName = sub.Plan?.Name ?? string.Empty,
                    StartDate = sub.StartDate,
                    EndDate = sub.EndDate,
                    IsActive = sub.IsActive,
                    PaymentStatus = sub.PaymentStatus
                }).ToList() ?? new();

            var activeSub = subscriptionsDto.FirstOrDefault(s => s.IsActive);

            return new CompanyDto
            {
                Id = company.Id,
                Name = company.Name,
                TaxId = company.TaxId,
                Address = company.Address,
                CreatedAt = company.CreatedAt,
                IsActive = company.IsActive,
                LogoUrl = company.LogoUrl,
                Subscriptions = subscriptionsDto,
                ActiveSubscription = activeSub
            };
        }
    }
}
