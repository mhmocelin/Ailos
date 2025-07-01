using Dapper;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Repositories.Interfaces;
using System.Data;

namespace Questao5.Infrastructure.Repositories;

public class ContaCorrenteRepository : IContaCorrenteRepository
{
    private readonly IDbConnection _connection;

    public ContaCorrenteRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<ContaCorrente?> ObterContaPorIdAsync(Guid id)
    {
        var result = await _connection.QueryFirstOrDefaultAsync<ContaCorrente>(
            "SELECT idcontacorrente as Id, numero as Numero, nome as Nome, ativo as Ativo FROM contacorrente WHERE idcontacorrente = @id",
            new { id });

        return result;
    }

    public async Task InserirMovimentoAsync(Guid idMovimento, Guid idConta, string tipo, decimal valor)
    {
        await _connection.ExecuteAsync(
            "INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) VALUES (@id, @idConta, @data, @tipo, @valor)",
            new
            {
                id = idMovimento.ToString(),
                idConta,
                data = DateTime.UtcNow.ToString("dd/MM/yyyy"),
                tipo,
                valor
            });
    }
}
