using MediatR;

namespace ESTADOTC.API.Application.CQRS.Commands.AddPayment;

public record AddPaymentCommand(
    int CardId,
    DateTime TransactionDate,
    decimal Amount
) : IRequest;