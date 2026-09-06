using AutoMapper;
using MediatR;
using ESTADOTC.API.Application.DTOs;
using ESTADOTC.API.Infrastructure.Repositories.Interfaces;

namespace ESTADOTC.API.Application.CQRS.Queries.GetTransactions;

public class GetTransactionsQueryHandler
    : IRequestHandler<
        GetTransactionsQuery,
        IEnumerable<TransactionDto>>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;

    public GetTransactionsQueryHandler(
        ITransactionRepository transactionRepository,
        IMapper mapper)
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TransactionDto>> Handle(
        GetTransactionsQuery request,
        CancellationToken cancellationToken)
    {
        var transactions = await _transactionRepository
            .GetTransactionsAsync(request.CardId);

        return _mapper.Map<IEnumerable<TransactionDto>>(transactions);
    }
}