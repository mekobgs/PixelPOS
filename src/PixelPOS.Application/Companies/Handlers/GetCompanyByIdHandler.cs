using MediatR;
using PixelPOS.Application.Companies.DTOs;
using PixelPOS.Application.Companies.Queries;
using PixelPOS.Application.Subscriptions.DTOs;
using PixelPOS.Domain.Repositories;

namespace PixelPOS.Application.Companies.Handlers
{
    public class GetCompanyByIdHandler : IRequestHandler<GetCompanyByIdQuery, CompanyDto?>
    {
        private readonly ICompanyRepository _companyRepository;

        public GetCompanyByIdHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<CompanyDto?> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(request.Id, cancellationToken);

            if (company == null || !company.IsActive)
                return null;

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
