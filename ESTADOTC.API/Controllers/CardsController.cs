using ESTADOTC.API.Application.CQRS.Commands.AddPayment;
using ESTADOTC.API.Application.CQRS.Commands.AddPurchase;
using ESTADOTC.API.Application.CQRS.Queries.GetCardFinancialSummary;
using ESTADOTC.API.Application.CQRS.Queries.GetCardStatement;
using ESTADOTC.API.Application.CQRS.Queries.GetCurrentMonthTransactions;
using ESTADOTC.API.Application.CQRS.Queries.GetMonthlyPurchaseTotals;
using ESTADOTC.API.Application.CQRS.Queries.GetTransactions;
using ESTADOTC.API.Application.DTOs;
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

    [HttpGet("{cardId}/transactions/current-month")]
    public async Task<IActionResult> GetCurrentMonthTransactions(
    int cardId,
    CancellationToken cancellationToken)
    {
        var query = new GetCurrentMonthTransactionsQuery(cardId);

        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpGet("{cardId}/monthly-purchase-totals")]
    public async Task<IActionResult> GetMonthlyPurchaseTotals(
        int cardId,
        CancellationToken cancellationToken)
    {
        var query = new GetMonthlyPurchaseTotalsQuery(cardId);

        var result = await _mediator.Send(query, cancellationToken);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("{cardId}/financial-summary")]
    public async Task<IActionResult> GetFinancialSummary(
        int cardId,
        CancellationToken cancellationToken)
    {
        var query = new GetCardFinancialSummaryQuery(cardId);

        var result = await _mediator.Send(query, cancellationToken);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost("{cardId}/purchases")]
    public async Task<IActionResult> AddPurchase(
    int cardId,
    [FromBody] AddPurchaseDto dto,
    CancellationToken cancellationToken)
    {
        var command = new AddPurchaseCommand(
            cardId,
            dto.TransactionDate,
            dto.Description,
            dto.Amount);

        await _mediator.Send(command, cancellationToken);

        return Ok();
    }

    [HttpPost("{cardId}/payments")]
    public async Task<IActionResult> AddPayment(
        int cardId,
        [FromBody] AddPaymentDto dto,
        CancellationToken cancellationToken)
    {
        var command = new AddPaymentCommand(
            cardId,
            dto.TransactionDate,
            dto.Amount);

        await _mediator.Send(command, cancellationToken);

        return Ok();
    }
}