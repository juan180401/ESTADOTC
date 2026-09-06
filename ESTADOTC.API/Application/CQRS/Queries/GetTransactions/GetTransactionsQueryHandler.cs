using MediatR;
using ESTADOTC.API.Domain.Entities;
using ESTADOTC.API.Infrastructure.Repositories.Interfaces;

namespace ESTADOTC.API.Application.CQRS.Queries.GetTransactions;

public class GetTransactionsQueryHandler
    : IRequestHandler<GetTransactionsQuery, IEnumerable<Transaction>>
{
    private readonly ITransactionRepository _transactionRepository;

    public GetTransactionsQueryHandler(
        ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<IEnumerable<Transaction>> Handle(
        GetTransactionsQuery request,
        CancellationToken cancellationToken)
    {
        return await _transactionRepository
            .GetTransactionsAsync(request.CardId);
    }
}