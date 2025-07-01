using Dapper;
using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Exceptions;
using System.Data;

namespace Questao5.Application.Handlers;

public class MovimentarContaHandler : IRequestHandler<MovimentarContaCommand, MovimentarContaResult>
{
    private readonly IDbConnection _connection;

    public MovimentarContaHandler(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<MovimentarContaResult> Handle(MovimentarContaCommand command, CancellationToken cancellationToken)
    {
        const string sqlConta = @"SELECT ativo FROM contacorrente WHERE idcontacorrente = @IdContaCorrente";
        var conta = await _connection.QuerySingleOrDefaultAsync<int?>(sqlConta, new { command.IdContaCorrente });

        if (conta == null)
            throw new BusinessException("Conta corrente não encontrada.", "INVALID_ACCOUNT");

        if (conta != 1)
            throw new BusinessException("Conta corrente está inativa.", "INACTIVE_ACCOUNT");

        if (command.Valor <= 0)
            throw new BusinessException("Valor deve ser positivo.", "INVALID_VALUE");

        if (command.TipoMovimento != 'C' && command.TipoMovimento != 'D')
            throw new BusinessException("Tipo de movimento inválido.", "INVALID_TYPE");

        const string sqlCheckIdempotencia = @"
            SELECT resultado FROM idempotencia WHERE chave_idempotencia = @ChaveIdempotencia;
        ";
        var resultadoExistente = await _connection.QuerySingleOrDefaultAsync<string>(sqlCheckIdempotencia, new { command.ChaveIdempotencia });

        if (resultadoExistente != null)
        {
            var idMovimento = Guid.Parse(resultadoExistente);
            return new MovimentarContaResult(idMovimento);
        }

        var idMovimentoNovo = Guid.NewGuid();
        var dataMovimento = DateTime.Now.ToString("dd/MM/yyyy");

        const string sqlInsertMovimento = @"
            INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor)
            VALUES (@IdMovimento, @IdContaCorrente, @DataMovimento, @TipoMovimento, @Valor);
        ";

        await _connection.ExecuteAsync(sqlInsertMovimento, new
        {
            IdMovimento = idMovimentoNovo,
            command.IdContaCorrente,
            DataMovimento = dataMovimento,
            command.TipoMovimento,
            command.Valor
        });

        const string sqlInsertIdempotencia = @"
            INSERT INTO idempotencia (chave_idempotencia, requisicao, resultado)
            VALUES (@ChaveIdempotencia, @Requisicao, @Resultado);
        ";

        var requisicaoJson = System.Text.Json.JsonSerializer.Serialize(command);

        await _connection.ExecuteAsync(sqlInsertIdempotencia, new
        {
            command.ChaveIdempotencia,
            Requisicao = requisicaoJson,
            Resultado = idMovimentoNovo.ToString()
        });

        return new MovimentarContaResult(idMovimentoNovo);
    }
}
