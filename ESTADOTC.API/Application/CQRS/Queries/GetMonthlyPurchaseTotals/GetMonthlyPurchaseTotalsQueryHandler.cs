using MediatR;
using ESTADOTC.API.Domain.Entities;
using ESTADOTC.API.Infrastructure.Repositories.Interfaces;

namespace ESTADOTC.API.Application.CQRS.Queries.GetMonthlyPurchaseTotals;

public class GetMonthlyPurchaseTotalsQueryHandler
    : IRequestHandler<
        GetMonthlyPurchaseTotalsQuery,
        MonthlyPurchaseTotals?>
{
    private readonly ICardRepository _cardRepository;

    public GetMonthlyPurchaseTotalsQueryHandler(
        ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }

    public async Task<MonthlyPurchaseTotals?> Handle(
        GetMonthlyPurchaseTotalsQuery request,
        CancellationToken cancellationToken)
    {
        return await _cardRepository
            .GetMonthlyPurchaseTotalsAsync(request.CardId);
    }
}