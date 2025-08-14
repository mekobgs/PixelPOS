using MediatR;
using PixelPOS.Application.Subscriptions.DTOs;

namespace PixelPOS.Application.Subscriptions.Queries
{
    public class GetCompanySubscriptionsQuery : IRequest<List<SubscriptionDto>>
    {
        public int CompanyId { get; set; }
    }
}
