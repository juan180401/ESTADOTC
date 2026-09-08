using ESTADOTC.API.Application.CQRS.Commands.AddPayment;
using ESTADOTC.API.Application.CQRS.Commands.AddPurchase;
using ESTADOTC.API.Application.CQRS.Queries.GetCardFinancialSummary;
using ESTADOTC.API.Application.CQRS.Queries.GetCardStatement;
using ESTADOTC.API.Application.CQRS.Queries.GetCurrentMonthTransactions;
using ESTADOTC.API.Application.CQRS.Queries.GetMonthlyPurchaseTotals;
using ESTADOTC.API.Application.CQRS.Queries.GetTransactions;
using ESTADOTC.API.Application.DTOs;
using ESTADOTC.API.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ESTADOTC.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CardsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly CardStatementPdfService _pdfService;

    public CardsController(
        IMediator mediator,
        CardStatementPdfService pdfService)
    {
        _mediator = mediator;
        _pdfService = pdfService;
    }

    [HttpGet("{cardId}/statement")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTransactions(
    int cardId,
    CancellationToken cancellationToken)
    {
        var query = new GetTransactionsQuery(cardId);

        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpGet("{cardId}/transactions/current-month")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCurrentMonthTransactions(
    int cardId,
    CancellationToken cancellationToken)
    {
        var query = new GetCurrentMonthTransactionsQuery(cardId);

        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpGet("{cardId}/monthly-purchase-totals")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    [HttpGet("{cardId}/statement/pdf")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCardStatementPdf(
    int cardId,
    CancellationToken cancellationToken)
    {
        var statement = await _mediator.Send(
            new GetCardStatementQuery(cardId),
            cancellationToken);

        if (statement is null)
        {
            return NotFound();
        }

        var financialSummary = await _mediator.Send(
            new GetCardFinancialSummaryQuery(cardId),
            cancellationToken);

        if (financialSummary is null)
        {
            return NotFound();
        }

        var monthlyTotals = await _mediator.Send(
            new GetMonthlyPurchaseTotalsQuery(cardId),
            cancellationToken);

        if (monthlyTotals is null)
        {
            return NotFound();
        }

        var transactions = await _mediator.Send(
            new GetCurrentMonthTransactionsQuery(cardId),
            cancellationToken);

        var pdfTransactions = transactions
            .Select(transaction => new PdfTransaction
            {
                TransactionDate = transaction.TransactionDate,
                Description = transaction.Description ?? string.Empty,
                TransactionType = transaction.TransactionType,
                Amount = transaction.Amount
            })
            .ToList();

        var pdf = _pdfService.Generate(
            statement.HolderName,
            statement.CardNumber,
            statement.CurrentBalance,
            statement.CreditLimit,
            statement.AvailableBalance,
            financialSummary.BonifiableInterest,
            financialSummary.MinimumPayment,
            financialSummary.TotalToPay,
            financialSummary.CashPaymentWithInterest,
            monthlyTotals.CurrentMonthPurchases,
            monthlyTotals.PreviousMonthPurchases,
            pdfTransactions
        );

        return File(
            pdf,
            "application/pdf",
            $"estado-cuenta-{cardId}.pdf"
        );
    }

    [HttpPost("{cardId}/purchases")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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