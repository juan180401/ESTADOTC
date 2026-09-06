using MediatR;
using ESTADOTC.API.Domain.Entities;

namespace ESTADOTC.API.Application.CQRS.Queries.GetMonthlyPurchaseTotals;

public record GetMonthlyPurchaseTotalsQuery(int CardId)
    : IRequest<MonthlyPurchaseTotals?>;