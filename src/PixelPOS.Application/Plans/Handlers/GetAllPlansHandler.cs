using MediatR;
using PixelPOS.Application.Plans.DTOs;
using PixelPOS.Application.Plans.Queries;
using PixelPOS.Domain.Repositories;

namespace PixelPOS.Application.Plans.Handlers
{
    public class GetAllPlansHandler : IRequestHandler<GetAllPlansQuery, PagedPlansDto>
    {
        private readonly IPlanRepository _planRepository;

        public GetAllPlansHandler(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<PagedPlansDto> Handle(GetAllPlansQuery request, CancellationToken cancellationToken)
        {
            var plans = await _planRepository.GetPagedAsync(request.Page, request.PageSize, request.Search, cancellationToken);
            var total = await _planRepository.GetTotalCountAsync(request.Search, cancellationToken);

            var data = plans.Select(plan => new PlanDto
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
            }).ToList();

            return new PagedPlansDto
            {
                Data = data,
                Page = request.Page,
                PageSize = request.PageSize,
                Total = total
            };
        }
    }
}
