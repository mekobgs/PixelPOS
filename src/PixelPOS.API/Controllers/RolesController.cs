using Microsoft.AspNetCore.Mvc;
using MediatR;
using PixelPOS.Application.Roles.Commands;
using PixelPOS.Application.Roles.Queries;

namespace PixelPOS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _mediator.Send(new GetAllRolesQuery());
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var role = await _mediator.Send(new GetRoleByIdQuery { Id = id });
            if (role == null)
                return NotFound(new { error = "Role not found." });
            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoleCommand command)
        {
            var role = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = role.Id }, role);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteRoleCommand { Id = id });
            return NoContent();
        }
    }
}
