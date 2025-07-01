using FluentValidation;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Services;
using Questao5.Domain.Exceptions;
using Questao5.Infrastructure.Repositories.Interfaces;

namespace Questao5.Domain.Services;

public class MovimentarContaService : IMovimentarContaService
{
    private readonly IContaCorrenteRepository _repository;
    private readonly IValidator<MovimentarContaCommand> _validator;

    public MovimentarContaService(
        IContaCorrenteRepository repository,
        IValidator<MovimentarContaCommand> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<MovimentarContaResult> MovimentarAsync(MovimentarContaCommand command)
    {
        var validation = await _validator.ValidateAsync(command);
        if (!validation.IsValid)
        {
            var error = validation.Errors.First();
            throw new BusinessException(error.ErrorMessage, error.ErrorCode);
        }

        var conta = await _repository.ObterContaPorIdAsync(command.IdContaCorrente);
        if (conta is null)
            throw new BusinessException("Conta corrente não encontrada.", "INVALID_ACCOUNT");

        conta.ValidarAtivacao();

        var idMovimento = Guid.NewGuid();
        await _repository.InserirMovimentoAsync(
            idMovimento,
            command.IdContaCorrente,
            command.TipoMovimento.ToString(),
            command.Valor);

        return new MovimentarContaResult(idMovimento);
    }
}
