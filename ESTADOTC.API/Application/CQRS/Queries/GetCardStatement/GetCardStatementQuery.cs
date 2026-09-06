using MediatR;
using ESTADOTC.API.Application.DTOs;

namespace ESTADOTC.API.Application.CQRS.Queries.GetCardStatement;

public record GetCardStatementQuery(int CardId)
    : IRequest<CardStatementDto?>;