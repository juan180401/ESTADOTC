using FluentValidation;
using ESTADOTC.API.Application.DTOs;

namespace ESTADOTC.API.Application.Validators;

public class AddPurchaseDtoValidator : AbstractValidator<AddPurchaseDto>
{
    public AddPurchaseDtoValidator()
    {
        RuleFor(x => x.TransactionDate)
            .NotEmpty()
            .WithMessage("La fecha de transacción es obligatoria.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(250)
            .WithMessage("La descripción es obligatoria y no puede superar 250 caracteres.");

        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("El monto debe ser mayor que cero.");
    }
}