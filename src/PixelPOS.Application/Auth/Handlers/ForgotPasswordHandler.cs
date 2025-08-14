using MediatR;
using PixelPOS.Application.Auth.Commands;
using PixelPOS.Domain.Security;
using PixelPOS.Domain.Repositories;


namespace PixelPOS.Application.Auth.Handlers
{
    public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public ForgotPasswordHandler(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<bool> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsernameAsync(request.UsernameOrEmail)
                ?? (await _userRepository.GetPagedAsync(0, 1, 1, request.UsernameOrEmail, cancellationToken)).FirstOrDefault();

            if (user == null)
                return false;

            var token = await _authService.GeneratePasswordResetToken(user);
            // Implementa aquí el envío del token por email/SMS según tu integración.

            return true;
        }
    }
}
