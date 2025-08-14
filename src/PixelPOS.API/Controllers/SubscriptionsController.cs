using Microsoft.AspNetCore.Mvc;
using MediatR;
using PixelPOS.Application.Subscriptions.Commands;
using PixelPOS.Application.Subscriptions.Queries;
using PixelPOS.Application.Subscriptions.DTOs;

namespace PixelPOS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubscriptionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("change-plan")]
        public async Task<IActionResult> ChangePlan([FromBody] ChangeCompanyPlanCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("company/{companyId}")]
        public async Task<ActionResult<List<SubscriptionDto>>> GetCompanySubscriptions(int companyId)
        {
            var subscriptions = await _mediator.Send(new GetCompanySubscriptionsQuery { CompanyId = companyId });
            return Ok(subscriptions);
        }
    }
}
