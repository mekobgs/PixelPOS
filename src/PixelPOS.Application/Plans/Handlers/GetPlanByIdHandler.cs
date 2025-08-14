using MediatR;
using PixelPOS.Application.Plans.DTOs;
using PixelPOS.Application.Plans.Queries;
using PixelPOS.Domain.Repositories;

namespace PixelPOS.Application.Plans.Handlers
{
    public class GetPlanByIdHandler : IRequestHandler<GetPlanByIdQuery, PlanDto?>
    {
        private readonly IPlanRepository _planRepository;

        public GetPlanByIdHandler(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<PlanDto?> Handle(GetPlanByIdQuery request, CancellationToken cancellationToken)
        {
            var plan = await _planRepository.GetByIdAsync(request.Id, cancellationToken);

            if (plan == null || !plan.IsActive)
                return null;

            return new PlanDto
            {
                Id = plan.Id,
                Name = plan.Name,
                Price = plan.Price,
                MaxUsers = plan.MaxUsers,
                MaxBranches = plan.MaxBranches,
                MonthlyInvoiceLimit = plan.MonthlyInvoiceLimit,
                EnableElectronicInvoicing = plan.EnableElectronicInvoicing,
                EnableReports = plan.EnableReports,
                EnableIntegrations = plan.EnableIntegrations,
                Description = plan.Description,
                IsActive = plan.IsActive
            };
        }
    }
}
