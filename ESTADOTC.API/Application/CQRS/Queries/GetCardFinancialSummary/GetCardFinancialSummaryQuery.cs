using MediatR;
using ESTADOTC.API.Domain.Entities;

namespace ESTADOTC.API.Application.CQRS.Queries.GetCardFinancialSummary;

public record GetCardFinancialSummaryQuery(int CardId)
    : IRequest<CardFinancialSummary?>;