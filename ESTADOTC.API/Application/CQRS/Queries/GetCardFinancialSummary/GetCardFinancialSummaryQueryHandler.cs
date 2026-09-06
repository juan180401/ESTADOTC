using MediatR;
using ESTADOTC.API.Domain.Entities;
using ESTADOTC.API.Infrastructure.Repositories.Interfaces;

namespace ESTADOTC.API.Application.CQRS.Queries.GetCardFinancialSummary;

public class GetCardFinancialSummaryQueryHandler
    : IRequestHandler<
        GetCardFinancialSummaryQuery,
        CardFinancialSummary?>
{
    private readonly ICardRepository _cardRepository;

    public GetCardFinancialSummaryQueryHandler(
        ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }

    public async Task<CardFinancialSummary?> Handle(
        GetCardFinancialSummaryQuery request,
        CancellationToken cancellationToken)
    {
        return await _cardRepository
            .GetFinancialSummaryAsync(request.CardId);
    }
}