using FluentValidation;
using ESTADOTC.API.Application.DTOs;

namespace ESTADOTC.API.Application.Validators;

public class AddPaymentDtoValidator : AbstractValidator<AddPaymentDto>
{
    public AddPaymentDtoValidator()
    {
        RuleFor(x => x.TransactionDate)
            .NotEmpty()
            .WithMessage("La fecha de transacción es obligatoria.");

        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("El monto debe ser mayor que cero.");
    }
}