using MediatR;
using PixelPOS.Application.Subscriptions.Queries;
using PixelPOS.Application.Subscriptions.DTOs;
using PixelPOS.Domain.Repositories;

namespace PixelPOS.Application.Subscriptions.Handlers
{
    public class GetCompanySubscriptionsHandler : IRequestHandler<GetCompanySubscriptionsQuery, List<SubscriptionDto>>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public GetCompanySubscriptionsHandler(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<List<SubscriptionDto>> Handle(GetCompanySubscriptionsQuery request, CancellationToken cancellationToken)
        {
            var subscriptions = await _subscriptionRepository.GetByCompanyIdAsync(request.CompanyId, cancellationToken);

            return subscriptions.Select(sub => new SubscriptionDto
            {
                Id = sub.Id,
                CompanyId = sub.CompanyId,
                PlanId = sub.PlanId,
                PlanName = sub.Plan?.Name ?? string.Empty,
                StartDate = sub.StartDate,
                EndDate = sub.EndDate,
                IsActive = sub.IsActive,
                PaymentStatus = sub.PaymentStatus
            }).ToList();
        }
    }
}
