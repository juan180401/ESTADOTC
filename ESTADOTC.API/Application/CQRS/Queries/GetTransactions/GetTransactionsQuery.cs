using MediatR;
using ESTADOTC.API.Domain.Entities;

namespace ESTADOTC.API.Application.CQRS.Queries.GetTransactions;

public record GetTransactionsQuery(int CardId)
    : IRequest<IEnumerable<Transaction>>;