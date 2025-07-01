using Dapper;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Repositories.Interfaces;
using System.Data;

namespace Questao5.Infrastructure.Repositories;

public class SaldoContaCorrenteRepository : ISaldoContaCorrenteRepository
{
    private readonly IDbConnection _connection;

    public SaldoContaCorrenteRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<ContaCorrente?> ObterContaPorIdAsync(Guid id)
    {
        const string sql = @"
            SELECT idcontacorrente as Id, numero, nome, ativo 
            FROM contacorrente 
            WHERE idcontacorrente = @id;
        ";

        return await _connection.QueryFirstOrDefaultAsync<ContaCorrente>(sql, new { id });
    }

    public async Task<decimal> CalcularSaldoAsync(Guid idContaCorrente)
    {
        const string sql = @"
            SELECT 
                SUM(CASE WHEN tipomovimento = 'C' THEN valor ELSE 0 END) AS Creditos,
                SUM(CASE WHEN tipomovimento = 'D' THEN valor ELSE 0 END) AS Debitos
            FROM movimento
            WHERE idcontacorrente = @idContaCorrente;
        ";

        var result = await _connection.QuerySingleOrDefaultAsync(sql, new { idContaCorrente });
        decimal creditos = result?.Creditos ?? 0;
        decimal debitos = result?.Debitos ?? 0;

        return creditos - debitos;
    }
}