using ESTADOTC.API.Application.CQRS.Queries.GetCardStatement;
using ESTADOTC.API.Application.CQRS.Queries.GetTransactions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("{cardId}/transactions")]
    public async Task<IActionResult> GetTransactions(
    int cardId,
    CancellationToken cancellationToken)
    {
        var query = new GetTransactionsQuery(cardId);

        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }
}