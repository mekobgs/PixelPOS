using MediatR;
using PixelPOS.Application.Branches.Commands;
using PixelPOS.Application.Branches.DTOs;
using PixelPOS.Domain.Entities;
using PixelPOS.Domain.Repositories;

namespace PixelPOS.Application.Branches.Handlers
{
    public class CreateBranchHandler : IRequestHandler<CreateBranchCommand, BranchDto>
    {
        private readonly IBranchRepository _branchRepository;
        private readonly ICompanyRepository _companyRepository;

        public CreateBranchHandler(IBranchRepository branchRepository, ICompanyRepository companyRepository)
        {
            _branchRepository = branchRepository;
            _companyRepository = companyRepository;
        }

        public async Task<BranchDto> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(request.CompanyId, cancellationToken);
            if (company == null || !company.IsActive)
                throw new Exception("Company not found or inactive.");

            if (await _branchRepository.ExistsByNameAsync(request.CompanyId, request.Name, null, cancellationToken))
                throw new Exception("A branch with the same name already exists for this company.");

            var branch = new Branch
            {
                CompanyId = request.CompanyId,
                Name = request.Name,
                Address = request.Address,
                Phone = request.Phone,
                Email = request.Email,
                PrinterName = request.PrinterName,
                OpeningHours = request.OpeningHours,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _branchRepository.AddAsync(branch, cancellationToken);
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
