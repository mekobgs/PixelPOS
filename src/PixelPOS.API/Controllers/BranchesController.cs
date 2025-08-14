using Microsoft.AspNetCore.Mvc;
using MediatR;
using PixelPOS.Application.Branches.Commands;
using PixelPOS.Application.Branches.Queries;

namespace PixelPOS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BranchesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BranchesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("by-company/{companyId}")]
        public async Task<IActionResult> GetAll(int companyId, [FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string? search = null)
        {
            var query = new GetAllBranchesQuery { CompanyId = companyId, Page = page, PageSize = pageSize, Search = search };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var branch = await _mediator.Send(new GetBranchByIdQuery { Id = id });
            if (branch == null)
                return NotFound(new { error = "Branch not found." });
            return Ok(branch);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBranchCommand command)
        {
            var branch = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = branch.Id }, branch);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBranchCommand command)
        {
            if (id != command.Id)
                return BadRequest(new { error = "Id does not match." });
            var branch = await _mediator.Send(command);
            return Ok(branch);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteBranchCommand { Id = id });
            return NoContent();
        }
    }
}
