using Dapper;
using MediatR;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Exceptions;
using System.Data;

namespace Questao5.Application.Handlers;
public class SaldoContaCorrenteHandler : IRequestHandler<SaldoContaCorrenteQuery, SaldoContaCorrenteResult>
{
    private readonly IDbConnection _connection;

    public SaldoContaCorrenteHandler(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<SaldoContaCorrenteResult> Handle(SaldoContaCorrenteQuery request, CancellationToken cancellationToken)
    {
        const string sqlConta = @"
            SELECT numero, nome, ativo
            FROM contacorrente
            WHERE idcontacorrente = @IdContaCorrente;
        ";

        var conta = await _connection.QuerySingleOrDefaultAsync<dynamic>(sqlConta, new { request.IdContaCorrente });

        if (conta == null)
            throw new BusinessException("Conta corrente não encontrada.", "INVALID_ACCOUNT");

        if (conta.ativo != 1)
            throw new BusinessException("Conta corrente está inativa.", "INACTIVE_ACCOUNT");

        const string sqlSaldo = @"
            SELECT 
                SUM(CASE WHEN tipomovimento = 'C' THEN valor ELSE 0 END) AS Creditos,
                SUM(CASE WHEN tipomovimento = 'D' THEN valor ELSE 0 END) AS Debitos
            FROM movimento
            WHERE idcontacorrente = @IdContaCorrente;
        ";

        var saldoResult = await _connection.QuerySingleOrDefaultAsync(sqlSaldo, new { request.IdContaCorrente });

        decimal creditos = saldoResult?.Creditos ?? 0;
        decimal debitos = saldoResult?.Debitos ?? 0;
        decimal saldo = creditos - debitos;

        return new SaldoContaCorrenteResult
        (
            conta.numero,
            conta.nome,
            DateTime.Now,
            saldo
        );
    }
}

