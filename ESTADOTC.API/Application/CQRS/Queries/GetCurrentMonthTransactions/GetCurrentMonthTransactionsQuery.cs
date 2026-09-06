using MediatR;
using ESTADOTC.API.Domain.Entities;

namespace ESTADOTC.API.Application.CQRS.Queries.GetCurrentMonthTransactions;

public record GetCurrentMonthTransactionsQuery(int CardId)
    : IRequest<IEnumerable<Transaction>>;