using MediatR;
using PixelPOS.Application.Companies.Commands;
using PixelPOS.Application.Companies.DTOs;
using PixelPOS.Application.Subscriptions.DTOs;
using PixelPOS.Domain.Entities;
using PixelPOS.Domain.Repositories;

namespace PixelPOS.Application.Companies.Handlers
{
    public class CreateCompanyHandler : IRequestHandler<CreateCompanyCommand, CompanyDto>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IPlanRepository _planRepository;

        public CreateCompanyHandler(ICompanyRepository companyRepository, IPlanRepository planRepository)
        {
            _companyRepository = companyRepository;
            _planRepository = planRepository;
        }

        public async Task<CompanyDto> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            if (await _companyRepository.ExistsByNameAsync(request.Name, null, cancellationToken))
                throw new Exception("A company with the same name already exists.");

            var plan = await _planRepository.GetByIdAsync(request.PlanId, cancellationToken);

            if (plan == null || !plan.IsActive)
                throw new Exception("Selected plan does not exist or is inactive.");

            var company = new Company
            {
                Name = request.Name,
                TaxId = request.TaxId,
                Address = request.Address,
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
                LogoUrl = request.LogoUrl,
                Subscriptions = new System.Collections.Generic.List<Subscription>()
            };

            var subscription = new Subscription
            {
                Company = company,
                Plan = plan,
                StartDate = DateTime.UtcNow,
                EndDate = null,
                IsActive = true,
                PaymentStatus = request.PaymentStatus ?? "Trial"
            };

            company.Subscriptions.Add(subscription);

            await _companyRepository.AddAsync(company, cancellationToken);
            await _companyRepository.SaveChangesAsync(cancellationToken);

            var subscriptionDto = new SubscriptionDto
            {
                Id = subscription.Id,
                CompanyId = company.Id,
                PlanId = plan.Id,
                PlanName = plan.Name,
                StartDate = subscription.StartDate,
                EndDate = subscription.EndDate,
                IsActive = subscription.IsActive,
                PaymentStatus = subscription.PaymentStatus
            };

            return new CompanyDto
            {
                Id = company.Id,
                Name = company.Name,
                TaxId = company.TaxId,
                Address = company.Address,
                CreatedAt = company.CreatedAt,
                IsActive = company.IsActive,
                LogoUrl = company.LogoUrl,
                Subscriptions = new System.Collections.Generic.List<SubscriptionDto> { subscriptionDto },
                ActiveSubscription = subscriptionDto
            };
        }
    }
}
