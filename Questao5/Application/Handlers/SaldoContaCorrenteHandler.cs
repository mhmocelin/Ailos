using Dapper;
using Questao5.Application.Mediator.Interfaces;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Exceptions;
using Questao5.Infrastructure.Repositories.Interfaces;
using System.Data;

namespace Questao5.Application.Handlers;
public class SaldoContaCorrenteHandler : IRequestHandler<SaldoContaCorrenteQuery, SaldoContaCorrenteResult>
{
    private readonly ISaldoContaCorrenteRepository _repository;

    public SaldoContaCorrenteHandler(ISaldoContaCorrenteRepository repository)
    {
        _repository = repository;
    }

    public async Task<SaldoContaCorrenteResult> Handle(SaldoContaCorrenteQuery request, CancellationToken cancellationToken)
    {
        var conta = await _repository.ObterContaPorIdAsync(request.IdContaCorrente);

        if (conta is null)
            throw new BusinessException("Conta corrente não encontrada.", "INVALID_ACCOUNT");

        conta.ValidarAtivacao();

        var saldo = await _repository.CalcularSaldoAsync(request.IdContaCorrente);

        return new SaldoContaCorrenteResult(
            conta.Numero,
            conta.Nome,
            DateTime.Now,
            saldo
        );
    }
}

