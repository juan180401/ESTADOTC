using AutoMapper;
using MediatR;
using ESTADOTC.API.Application.DTOs;
using ESTADOTC.API.Infrastructure.Repositories.Interfaces;

namespace ESTADOTC.API.Application.CQRS.Queries.GetCardStatement;

public class GetCardStatementQueryHandler
    : IRequestHandler<
        GetCardStatementQuery,
        CardStatementDto?>
{
    private readonly ICardRepository _cardRepository;
    private readonly IMapper _mapper;

    public GetCardStatementQueryHandler(
        ICardRepository cardRepository,
        IMapper mapper)
    {
        _cardRepository = cardRepository;
        _mapper = mapper;
    }

    public async Task<CardStatementDto?> Handle(
        GetCardStatementQuery request,
        CancellationToken cancellationToken)
    {
        var card = await _cardRepository
            .GetCardStatementAsync(request.CardId);

        return card is null
            ? null
            : _mapper.Map<CardStatementDto>(card);
    }
}