using MediatR;
using PixelPOS.Application.Auth.Commands;
using PixelPOS.Domain.Security;

namespace PixelPOS.Application.Auth.Handlers
{
    public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, bool>
    {
        private readonly IAuthService _authService;

        public ResetPasswordHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            return await _authService.ResetPassword(request.UserId, request.NewPassword, request.Token);
        }
    }
}
