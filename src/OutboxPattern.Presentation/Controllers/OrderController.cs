using MediatR;
using Microsoft.AspNetCore.Mvc;
using OutboxPattern.Application.Features.Orders.Create;

namespace OutboxPattern.Presentation.Controllers;

[Route("api/[controller]s")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] OrderCreateCommand orderCreate)
    {
        var orderRespnse = await _mediator.Send(orderCreate);

        return orderRespnse.IsSuccess ? Ok(orderRespnse) : BadRequest(orderRespnse);
    }

}
