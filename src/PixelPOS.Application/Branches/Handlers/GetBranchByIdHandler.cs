using MediatR;
using PixelPOS.Application.Branches.DTOs;
using PixelPOS.Application.Branches.Queries;
using PixelPOS.Domain.Repositories;

namespace PixelPOS.Application.Branches.Handlers
{
    public class GetBranchByIdHandler : IRequestHandler<GetBranchByIdQuery, BranchDto?>
    {
        private readonly IBranchRepository _branchRepository;

        public GetBranchByIdHandler(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public async Task<BranchDto?> Handle(GetBranchByIdQuery request, CancellationToken cancellationToken)
        {
            var branch = await _branchRepository.GetByIdAsync(request.Id, cancellationToken);

            if (branch == null || !branch.IsActive)
                return null;

            return new BranchDto
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
            };
        }
    }
}
