using MediatR;
using ESTADOTC.API.Infrastructure.Repositories.Interfaces;

namespace ESTADOTC.API.Application.CQRS.Commands.AddPayment;

public class AddPaymentCommandHandler
    : IRequestHandler<AddPaymentCommand>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ICardRepository _cardRepository;

    public AddPaymentCommandHandler(
        ITransactionRepository transactionRepository,
        ICardRepository cardRepository)
    {
        _transactionRepository = transactionRepository;
        _cardRepository = cardRepository;
    }

    public async Task Handle(
        AddPaymentCommand request,
        CancellationToken cancellationToken)
    {
        if (request.Amount <= 0)
        {
            throw new InvalidOperationException(
                "El monto del pago debe ser mayor que cero.");
        }

        var financialSummary =
            await _cardRepository.GetFinancialSummaryAsync(
                request.CardId);

        if (financialSummary is null)
        {
            throw new KeyNotFoundException(
                "La tarjeta indicada no existe.");
        }

        if (request.Amount > financialSummary.CurrentBalance)
        {
            throw new InvalidOperationException(
                $"El pago no puede superar el saldo actual de {financialSummary.CurrentBalance:C}.");
        }

        await _transactionRepository.AddPaymentAsync(
            request.CardId,
            request.TransactionDate,
            request.Amount);
    }
}