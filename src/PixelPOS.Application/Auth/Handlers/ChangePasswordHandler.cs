using MediatR;
using PixelPOS.Application.Auth.Commands;
using PixelPOS.Domain.Security;

namespace PixelPOS.Application.Auth.Handlers
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, bool>
    {
        private readonly IAuthService _authService;

        public ChangePasswordHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            return await _authService.ChangePassword(request.UserId, request.OldPassword, request.NewPassword);
        }
    }
}
