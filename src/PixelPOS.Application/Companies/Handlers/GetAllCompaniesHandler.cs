using MediatR;
using PixelPOS.Application.Companies.DTOs;
using PixelPOS.Application.Companies.Queries;
using PixelPOS.Application.Subscriptions.DTOs;
using PixelPOS.Domain.Repositories;

namespace PixelPOS.Application.Companies.Handlers
{
    public class GetAllCompaniesHandler : IRequestHandler<GetAllCompaniesQuery, PagedCompaniesDto>
    {
        private readonly ICompanyRepository _companyRepository;

        public GetAllCompaniesHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<PagedCompaniesDto> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
        {
            var companies = await _companyRepository.GetPagedAsync(request.Page, request.PageSize, request.Search, cancellationToken);
            var total = await _companyRepository.GetTotalCountAsync(request.Search, cancellationToken);

            var data = companies.Select(company =>
            {
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
            }).ToList();

            return new PagedCompaniesDto
            {
                Data = data,
                Page = request.Page,
                PageSize = request.PageSize,
                Total = total
            };
        }
    }
}
