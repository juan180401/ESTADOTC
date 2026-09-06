using MediatR;
using ESTADOTC.API.Application.DTOs;

namespace ESTADOTC.API.Application.CQRS.Queries.GetMonthlyPurchaseTotals;

public record GetMonthlyPurchaseTotalsQuery(int CardId)
    : IRequest<MonthlyPurchaseTotalsDto?>;