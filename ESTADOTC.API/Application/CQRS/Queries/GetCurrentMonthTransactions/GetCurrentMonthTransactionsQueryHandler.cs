using MediatR;
using ESTADOTC.API.Domain.Entities;
using ESTADOTC.API.Infrastructure.Repositories.Interfaces;

namespace ESTADOTC.API.Application.CQRS.Queries.GetCurrentMonthTransactions;

public class GetCurrentMonthTransactionsQueryHandler
    : IRequestHandler<
        GetCurrentMonthTransactionsQuery,
        IEnumerable<Transaction>>
{
    private readonly ITransactionRepository _transactionRepository;

    public GetCurrentMonthTransactionsQueryHandler(
        ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<IEnumerable<Transaction>> Handle(
        GetCurrentMonthTransactionsQuery request,
        CancellationToken cancellationToken)
    {
        return await _transactionRepository
            .GetCurrentMonthTransactionsAsync(request.CardId);
    }
}