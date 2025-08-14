using MediatR;
using PixelPOS.Application.Subscriptions.Commands;
using PixelPOS.Application.Subscriptions.DTOs;
using PixelPOS.Domain.Entities;
using PixelPOS.Domain.Repositories;

namespace PixelPOS.Application.Subscriptions.Handlers
{
    public class ChangeCompanyPlanHandler : IRequestHandler<ChangeCompanyPlanCommand, SubscriptionDto>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IPlanRepository _planRepository;

        public ChangeCompanyPlanHandler(
            ISubscriptionRepository subscriptionRepository,
            ICompanyRepository companyRepository,
            IPlanRepository planRepository)
        {
            _subscriptionRepository = subscriptionRepository;
            _companyRepository = companyRepository;
            _planRepository = planRepository;
        }

        public async Task<SubscriptionDto> Handle(ChangeCompanyPlanCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(request.CompanyId, cancellationToken);

            if (company == null || !company.IsActive)
                throw new Exception("Company not found or inactive.");

            var newPlan = await _planRepository.GetByIdAsync(request.NewPlanId, cancellationToken);

            if (newPlan == null || !newPlan.IsActive)
                throw new Exception("The new plan does not exist or is inactive.");

            var activeSubscription = await _subscriptionRepository.GetActiveByCompanyIdAsync(request.CompanyId, cancellationToken);

            if (activeSubscription != null)
            {
                activeSubscription.IsActive = false;
                activeSubscription.EndDate = DateTime.UtcNow;
                await _subscriptionRepository.UpdateAsync(activeSubscription, cancellationToken);
            }

            var newSubscription = new Subscription
            {
                CompanyId = company.Id,
                PlanId = newPlan.Id,
                StartDate = DateTime.UtcNow,
                EndDate = null,
                IsActive = true,
                PaymentStatus = request.PaymentStatus ?? "Paid"
            };

            await _subscriptionRepository.AddAsync(newSubscription, cancellationToken);
            await _subscriptionRepository.SaveChangesAsync(cancellationToken);

            return new SubscriptionDto
            {
                Id = newSubscription.Id,
                CompanyId = company.Id,
                PlanId = newPlan.Id,
                PlanName = newPlan.Name,
                StartDate = newSubscription.StartDate,
                EndDate = newSubscription.EndDate,
                IsActive = newSubscription.IsActive,
                PaymentStatus = newSubscription.PaymentStatus
            };
        }
    }
}
