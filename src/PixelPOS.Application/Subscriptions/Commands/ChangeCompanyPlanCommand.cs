using MediatR;
using PixelPOS.Application.Subscriptions.DTOs;

namespace PixelPOS.Application.Subscriptions.Commands
{
    public class ChangeCompanyPlanCommand : IRequest<SubscriptionDto>
    {
        public int CompanyId { get; set; }
        public int NewPlanId { get; set; }
        public string? PaymentStatus { get; set; }
    }
}
