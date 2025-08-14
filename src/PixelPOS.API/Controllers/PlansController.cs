using Microsoft.AspNetCore.Mvc;
using MediatR;
using PixelPOS.Application.Plans.Commands;
using PixelPOS.Application.Plans.Queries;

namespace PixelPOS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlansController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PlansController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string? search = null)
        {
            var query = new GetAllPlansQuery { Page = page, PageSize = pageSize, Search = search };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var plan = await _mediator.Send(new GetPlanByIdQuery { Id = id });
            if (plan == null)
                return NotFound(new { error = "Plan not found." });
            return Ok(plan);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePlanCommand command)
        {
            var plan = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = plan.Id }, plan);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePlanCommand command)
        {
            if (id != command.Id)
                return BadRequest(new { error = "Id does not match." });
            var plan = await _mediator.Send(command);
            return Ok(plan);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeletePlanCommand { Id = id });
            return NoContent();
        }
    }
}
