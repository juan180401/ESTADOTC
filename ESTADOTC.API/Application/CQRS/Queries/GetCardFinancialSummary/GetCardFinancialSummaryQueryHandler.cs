using AutoMapper;
using MediatR;
using ESTADOTC.API.Application.DTOs;
using ESTADOTC.API.Infrastructure.Repositories.Interfaces;

namespace ESTADOTC.API.Application.CQRS.Queries.GetCardFinancialSummary;

public class GetCardFinancialSummaryQueryHandler
    : IRequestHandler<
        GetCardFinancialSummaryQuery,
        CardFinancialSummaryDto?>
{
    private readonly ICardRepository _cardRepository;
    private readonly IMapper _mapper;

    public GetCardFinancialSummaryQueryHandler(
        ICardRepository cardRepository,
        IMapper mapper)
    {
        _cardRepository = cardRepository;
        _mapper = mapper;
    }

    public async Task<CardFinancialSummaryDto?> Handle(
        GetCardFinancialSummaryQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _cardRepository
            .GetFinancialSummaryAsync(request.CardId);

        return result is null
            ? null
            : _mapper.Map<CardFinancialSummaryDto>(result);
    }
}