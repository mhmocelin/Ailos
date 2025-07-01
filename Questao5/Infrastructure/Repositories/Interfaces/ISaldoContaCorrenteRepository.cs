using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Repositories.Interfaces;

public interface ISaldoContaCorrenteRepository
{
    Task<ContaCorrente?> ObterContaPorIdAsync(Guid id);
    Task<decimal> CalcularSaldoAsync(Guid idContaCorrente);
}
