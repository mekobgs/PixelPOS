using Microsoft.AspNetCore.Mvc;
using MediatR;
using PixelPOS.Application.Companies.Commands;
using PixelPOS.Application.Companies.Queries;

namespace PixelPOS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompaniesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string? search = null)
        {
            var query = new GetAllCompaniesQuery { Page = page, PageSize = pageSize, Search = search };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var company = await _mediator.Send(new GetCompanyByIdQuery { Id = id });
            if (company == null)
                return NotFound(new { error = "Company not found." });
            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCompanyCommand command)
        {
            var company = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = company.Id }, company);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCompanyCommand command)
        {
            if (id != command.Id)
                return BadRequest(new { error = "Id does not match." });
            var company = await _mediator.Send(command);
            return Ok(company);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteCompanyCommand { Id = id });
            return NoContent();
        }
    }
}
