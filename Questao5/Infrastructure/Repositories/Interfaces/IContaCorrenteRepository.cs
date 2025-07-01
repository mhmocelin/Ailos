using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Repositories.Interfaces;

public interface IContaCorrenteRepository
{
    Task<ContaCorrente?> ObterContaPorIdAsync(Guid id);
    Task InserirMovimentoAsync(Guid idMovimento, Guid idConta, string tipo, decimal valor);
}
