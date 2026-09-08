using MediatR;
using ESTADOTC.API.Infrastructure.Repositories.Interfaces;

namespace ESTADOTC.API.Application.CQRS.Commands.AddPurchase;

public class AddPurchaseCommandHandler
    : IRequestHandler<AddPurchaseCommand>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ICardRepository _cardRepository;

    public AddPurchaseCommandHandler(
        ITransactionRepository transactionRepository,
        ICardRepository cardRepository)
    {
        _transactionRepository = transactionRepository;
        _cardRepository = cardRepository;
    }

    public async Task Handle(
        AddPurchaseCommand request,
        CancellationToken cancellationToken)
    {
        if (request.Amount <= 0)
        {
            throw new InvalidOperationException(
                "El monto de la compra debe ser mayor que cero.");
        }

        var card =
            await _cardRepository.GetCardStatementAsync(
                request.CardId);

        if (card is null)
        {
            throw new KeyNotFoundException(
                "La tarjeta indicada no existe.");
        }

        if (request.Amount > card.AvailableBalance)
        {
            throw new InvalidOperationException(
                $"La compra no puede superar el crédito disponible de {card.AvailableBalance:C}.");
        }

        await _transactionRepository.AddPurchaseAsync(
            request.CardId,
            request.TransactionDate,
            request.Description,
            request.Amount);
    }
}