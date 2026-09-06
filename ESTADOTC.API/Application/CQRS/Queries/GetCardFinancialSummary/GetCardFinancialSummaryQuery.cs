using MediatR;
using ESTADOTC.API.Application.DTOs;

namespace ESTADOTC.API.Application.CQRS.Queries.GetCardFinancialSummary;

public record GetCardFinancialSummaryQuery(int CardId)
    : IRequest<CardFinancialSummaryDto?>;