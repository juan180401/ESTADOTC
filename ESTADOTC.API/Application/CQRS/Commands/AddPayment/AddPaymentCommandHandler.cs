using MediatR;
using ESTADOTC.API.Infrastructure.Repositories.Interfaces;

namespace ESTADOTC.API.Application.CQRS.Commands.AddPayment;

public class AddPaymentCommandHandler
    : IRequestHandler<AddPaymentCommand>
{
    private readonly ITransactionRepository _transactionRepository;

    public AddPaymentCommandHandler(
        ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task Handle(
        AddPaymentCommand request,
        CancellationToken cancellationToken)
    {
        await _transactionRepository.AddPaymentAsync(
            request.CardId,
            request.TransactionDate,
            request.Amount);
    }
}