using MediatR;
using ESTADOTC.API.Infrastructure.Repositories.Interfaces;

namespace ESTADOTC.API.Application.CQRS.Commands.AddPurchase;

public class AddPurchaseCommandHandler
    : IRequestHandler<AddPurchaseCommand>
{
    private readonly ITransactionRepository _transactionRepository;

    public AddPurchaseCommandHandler(
        ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task Handle(
        AddPurchaseCommand request,
        CancellationToken cancellationToken)
    {
        await _transactionRepository.AddPurchaseAsync(
            request.CardId,
            request.TransactionDate,
            request.Description,
            request.Amount);
    }
}