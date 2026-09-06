using MediatR;
using ESTADOTC.API.Domain.Entities;

namespace ESTADOTC.API.Application.CQRS.Queries.GetCardStatement;

public record GetCardStatementQuery(int CardId) : IRequest<Card?>;