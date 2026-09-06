using MediatR;
using ESTADOTC.API.Application.DTOs;

namespace ESTADOTC.API.Application.CQRS.Queries.GetCurrentMonthTransactions;

public record GetCurrentMonthTransactionsQuery(int CardId)
    : IRequest<IEnumerable<TransactionDto>>;