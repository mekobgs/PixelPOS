using MediatR;
using PixelPOS.Application.Branches.DTOs;
using PixelPOS.Application.Branches.Queries;
using PixelPOS.Domain.Repositories;

namespace PixelPOS.Application.Branches.Handlers
{
    public class GetAllBranchesHandler : IRequestHandler<GetAllBranchesQuery, PagedBranchesDto>
    {
        private readonly IBranchRepository _branchRepository;

        public GetAllBranchesHandler(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public async Task<PagedBranchesDto> Handle(GetAllBranchesQuery request, CancellationToken cancellationToken)
        {
            var branches = await _branchRepository.GetPagedAsync(request.CompanyId, request.Page, request.PageSize, request.Search, cancellationToken);
            var total = await _branchRepository.GetTotalCountAsync(request.CompanyId, request.Search, cancellationToken);

            var data = branches.Select(branch => new BranchDto
            {
                Id = branch.Id,
                CompanyId = branch.CompanyId,
                Name = branch.Name,
                Address = branch.Address,
                Phone = branch.Phone,
                Email = branch.Email,
                PrinterName = branch.PrinterName,
                OpeningHours = branch.OpeningHours,
                IsActive = branch.IsActive,
                CreatedAt = branch.CreatedAt,
                UpdatedAt = branch.UpdatedAt
            }).ToList();

            return new PagedBranchesDto
            {
                Data = data,
                Page = request.Page,
                PageSize = request.PageSize,
                Total = total
            };
        }
    }
}
