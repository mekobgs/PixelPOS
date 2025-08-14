using MediatR;
using PixelPOS.Application.Plans.Commands;
using PixelPOS.Domain.Repositories;

namespace PixelPOS.Application.Plans.Handlers
{
    public class DeletePlanHandler : IRequestHandler<DeletePlanCommand, Unit>
    {
        private readonly IPlanRepository _planRepository;

        public DeletePlanHandler(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<Unit> Handle(DeletePlanCommand request, CancellationToken cancellationToken)
        {
            var plan = await _planRepository.GetByIdAsync(request.Id, cancellationToken);

            if (plan == null)
                throw new Exception("Plan not found.");

            if (plan.Subscriptions != null && plan.Subscriptions.Any(s => s.IsActive))
                throw new Exception("Cannot delete plan: there are active subscriptions for this plan.");

            plan.IsActive = false;
            await _planRepository.UpdateAsync(plan, cancellationToken);
            await _planRepository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
