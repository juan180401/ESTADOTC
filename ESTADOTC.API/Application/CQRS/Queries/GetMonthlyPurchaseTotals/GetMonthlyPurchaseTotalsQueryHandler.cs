using AutoMapper;
using MediatR;
using ESTADOTC.API.Application.DTOs;
using ESTADOTC.API.Infrastructure.Repositories.Interfaces;

namespace ESTADOTC.API.Application.CQRS.Queries.GetMonthlyPurchaseTotals;

public class GetMonthlyPurchaseTotalsQueryHandler
    : IRequestHandler<
        GetMonthlyPurchaseTotalsQuery,
        MonthlyPurchaseTotalsDto?>
{
    private readonly ICardRepository _cardRepository;
    private readonly IMapper _mapper;

    public GetMonthlyPurchaseTotalsQueryHandler(
        ICardRepository cardRepository,
        IMapper mapper)
    {
        _cardRepository = cardRepository;
        _mapper = mapper;
    }

    public async Task<MonthlyPurchaseTotalsDto?> Handle(
        GetMonthlyPurchaseTotalsQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _cardRepository
            .GetMonthlyPurchaseTotalsAsync(request.CardId);

        return result is null
            ? null
            : _mapper.Map<MonthlyPurchaseTotalsDto>(result);
    }
}