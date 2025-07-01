using Questao5.Application.Commands.Requests;
using FluentValidation;

namespace Questao5.Application.Validators;

public class MovimentarContaCommandValidator : AbstractValidator<MovimentarContaCommand>
{
    public MovimentarContaCommandValidator()
    {
        RuleFor(x => x.IdContaCorrente)
            .NotEmpty().WithMessage("Conta corrente é obrigatória.");

        RuleFor(x => x.Valor)
            .GreaterThan(0).WithMessage("Valor deve ser maior que zero.")
            .WithErrorCode("INVALID_VALUE");

        RuleFor(x => x.TipoMovimento)
            .Must(x => x == 'C' || x == 'D')
            .WithMessage("Tipo de movimento deve ser 'C' ou 'D'.")
            .WithErrorCode("INVALID_TYPE");
    }
}
