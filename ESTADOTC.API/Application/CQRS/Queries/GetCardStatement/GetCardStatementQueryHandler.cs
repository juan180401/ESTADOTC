using MediatR;
using ESTADOTC.API.Domain.Entities;
using ESTADOTC.API.Infrastructure.Repositories.Interfaces;

namespace ESTADOTC.API.Application.CQRS.Queries.GetCardStatement;

public class GetCardStatementQueryHandler
    : IRequestHandler<GetCardStatementQuery, Card?>
{
    private readonly ICardRepository _cardRepository;

    public GetCardStatementQueryHandler(ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }

    public async Task<Card?> Handle(
        GetCardStatementQuery request,
        CancellationToken cancellationToken)
    {
        return await _cardRepository.GetCardStatementAsync(request.CardId);
    }
}