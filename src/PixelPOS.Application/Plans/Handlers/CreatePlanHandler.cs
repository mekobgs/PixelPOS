using MediatR;
using PixelPOS.Application.Plans.Commands;
using PixelPOS.Application.Plans.DTOs;
using PixelPOS.Domain.Entities;
using PixelPOS.Domain.Repositories;

namespace PixelPOS.Application.Plans.Handlers
{
    public class CreatePlanHandler : IRequestHandler<CreatePlanCommand, PlanDto>
    {
        private readonly IPlanRepository _planRepository;

        public CreatePlanHandler(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<PlanDto> Handle(CreatePlanCommand request, CancellationToken cancellationToken)
        {
            if (await _planRepository.ExistsByNameAsync(request.Name, null, cancellationToken))
                throw new System.Exception("A plan with the same name already exists.");

            var plan = new Plan
            {
                Name = request.Name,
                Price = request.Price,
                MaxUsers = request.MaxUsers,
                MaxBranches = request.MaxBranches,
                MonthlyInvoiceLimit = request.MonthlyInvoiceLimit,
                EnableElectronicInvoicing = request.EnableElectronicInvoicing,
                EnableReports = request.EnableReports,
                EnableIntegrations = request.EnableIntegrations,
                Description = request.Description,
                IsActive = true
            };

            await _planRepository.AddAsync(plan, cancellationToken);
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
