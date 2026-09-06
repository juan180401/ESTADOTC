using MediatR;

namespace ESTADOTC.API.Application.CQRS.Commands.AddPurchase;

public record AddPurchaseCommand(
    int CardId,
    DateTime TransactionDate,
    string Description,
    decimal Amount
) : IRequest;