using AutoMapper;
using MediatR;
using ESTADOTC.API.Application.DTOs;
using ESTADOTC.API.Infrastructure.Repositories.Interfaces;

namespace ESTADOTC.API.Application.CQRS.Queries.GetCurrentMonthTransactions;

public class GetCurrentMonthTransactionsQueryHandler
    : IRequestHandler<
        GetCurrentMonthTransactionsQuery,
        IEnumerable<TransactionDto>>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;

    public GetCurrentMonthTransactionsQueryHandler(
        ITransactionRepository transactionRepository,
        IMapper mapper)
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TransactionDto>> Handle(
        GetCurrentMonthTransactionsQuery request,
        CancellationToken cancellationToken)
    {
        var transactions = await _transactionRepository
            .GetCurrentMonthTransactionsAsync(request.CardId);

        return _mapper.Map<IEnumerable<TransactionDto>>(transactions);
    }
}