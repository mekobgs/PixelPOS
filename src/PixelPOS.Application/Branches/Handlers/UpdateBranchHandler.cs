using MediatR;
using PixelPOS.Application.Branches.Commands;
using PixelPOS.Application.Branches.DTOs;
using PixelPOS.Domain.Repositories;

namespace PixelPOS.Application.Branches.Handlers
{
    public class UpdateBranchHandler : IRequestHandler<UpdateBranchCommand, BranchDto>
    {
        private readonly IBranchRepository _branchRepository;

        public UpdateBranchHandler(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public async Task<BranchDto> Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
        {
            var branch = await _branchRepository.GetByIdAsync(request.Id, cancellationToken);

            if (branch == null)
                throw new Exception("Branch not found.");

            if (await _branchRepository.ExistsByNameAsync(request.CompanyId, request.Name, request.Id, cancellationToken))
                throw new Exception("A branch with the same name already exists for this company.");

            branch.Name = request.Name;
            branch.Address = request.Address;
            branch.Phone = request.Phone;
            branch.Email = request.Email;
            branch.PrinterName = request.PrinterName;
            branch.OpeningHours = request.OpeningHours;
            branch.IsActive = request.IsActive;
            branch.UpdatedAt = DateTime.UtcNow;

            await _branchRepository.UpdateAsync(branch, cancellationToken);
            await _branchRepository.SaveChangesAsync(cancellationToken);

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
