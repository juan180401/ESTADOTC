using MediatR;
using ESTADOTC.API.Application.DTOs;

namespace ESTADOTC.API.Application.CQRS.Queries.GetTransactions;

public record GetTransactionsQuery(int CardId)
    : IRequest<IEnumerable<TransactionDto>>;