using MediatR;
using Microsoft.AspNetCore.Mvc;
using OutboxPattern.Application.Features.Products.Create;

namespace OutboxPattern.Presentation.Controllers;

[Route("api/[controller]s")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ProductCreateCommand productCreate)
    {
        var productRespnse = await _mediator.Send(productCreate);

        return productRespnse.IsSuccess ? Ok(productRespnse) : BadRequest(productRespnse);
    }
}
