using MediatR;
using Microsoft.AspNetCore.Mvc;
using ESTADOTC.API.Application.CQRS.Queries.GetCardStatement;

namespace ESTADOTC.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CardsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CardsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{cardId}/statement")]
    public async Task<IActionResult> GetCardStatement(
        int cardId,
        CancellationToken cancellationToken)
    {
        var query = new GetCardStatementQuery(cardId);

        var result = await _mediator.Send(query, cancellationToken);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}