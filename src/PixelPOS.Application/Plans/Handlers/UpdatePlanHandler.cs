using MediatR;
using PixelPOS.Application.Plans.Commands;
using PixelPOS.Application.Plans.DTOs;
using PixelPOS.Domain.Repositories;

namespace PixelPOS.Application.Plans.Handlers
{
    public class UpdatePlanHandler : IRequestHandler<UpdatePlanCommand, PlanDto>
    {
        private readonly IPlanRepository _planRepository;

        public UpdatePlanHandler(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<PlanDto> Handle(UpdatePlanCommand request, CancellationToken cancellationToken)
        {
            var plan = await _planRepository.GetByIdAsync(request.Id, cancellationToken);

            if (plan == null)
                throw new System.Exception("Plan not found.");

            if (await _planRepository.ExistsByNameAsync(request.Name, request.Id, cancellationToken))
                throw new System.Exception("A plan with the same name already exists.");

            plan.Name = request.Name;
            plan.Price = request.Price;
            plan.MaxUsers = request.MaxUsers;
            plan.MaxBranches = request.MaxBranches;
            plan.MonthlyInvoiceLimit = request.MonthlyInvoiceLimit;
            plan.EnableElectronicInvoicing = request.EnableElectronicInvoicing;
            plan.EnableReports = request.EnableReports;
            plan.EnableIntegrations = request.EnableIntegrations;
            plan.Description = request.Description;
            plan.IsActive = request.IsActive;

            await _planRepository.UpdateAsync(plan, cancellationToken);
            await _planRepository.SaveChangesAsync(cancellationToken);

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
