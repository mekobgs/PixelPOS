using Microsoft.AspNetCore.Mvc;
using MediatR;
using PixelPOS.Application.Auth.Commands;
using PixelPOS.Application.Auth.DTOs;
using System.Threading.Tasks;

namespace PixelPOS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result) return NotFound(new { error = "User not found or unable to send reset token." });
            return Ok(new { message = "Reset token sent (implement email/SMS delivery)." });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result) return BadRequest(new { error = "Invalid or expired reset token." });
            return Ok(new { message = "Password reset successful." });
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result) return BadRequest(new { error = "Current password incorrect." });
            return Ok(new { message = "Password changed successfully." });
        }
    }
}
