using MediatR;
using PixelPOS.Application.Branches.Commands;
using PixelPOS.Domain.Repositories;

namespace PixelPOS.Application.Branches.Handlers
{
    public class DeleteBranchHandler : IRequestHandler<DeleteBranchCommand, Unit>
    {
        private readonly IBranchRepository _branchRepository;

        public DeleteBranchHandler(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public async Task<Unit> Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
        {
            var branch = await _branchRepository.GetByIdAsync(request.Id, cancellationToken);

            if (branch == null)
                throw new Exception("Branch not found.");

            branch.IsActive = false;
            branch.UpdatedAt = DateTime.UtcNow;

            await _branchRepository.UpdateAsync(branch, cancellationToken);
            await _branchRepository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
