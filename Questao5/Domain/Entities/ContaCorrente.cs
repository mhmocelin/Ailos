using Questao5.Domain.Exceptions;

namespace Questao5.Domain.Entities;

public class ContaCorrente
{
    public Guid IdContaCorrente { get; set; }
    public int Numero { get; set; }
    public string Nome { get; set; } = string.Empty;
    public bool Ativo { get; set; }

    public ICollection<Movimento> Movimentos { get; set; } = new List<Movimento>();

    public void ValidarAtivacao()
    {
        if (!Ativo)
            throw new BusinessException("Conta corrente inativa.", "INACTIVE_ACCOUNT");
    }
}
