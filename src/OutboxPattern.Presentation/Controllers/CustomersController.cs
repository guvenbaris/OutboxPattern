using MediatR;
using Microsoft.AspNetCore.Mvc;
using OutboxPattern.Application.Features.Customers.Create;

namespace OutboxPattern.Presentation.Controllers;

[Route("api/[controller]s")]
[ApiController]
public class CustomersController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CustomerCreateCommand customerCreateCommand)
    {
        var customerResponse = await _mediator.Send(customerCreateCommand);

        return customerResponse.IsSuccess ? Ok(customerResponse) : BadRequest(customerResponse);
    }
}
