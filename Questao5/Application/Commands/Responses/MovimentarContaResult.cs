namespace Questao5.Application.Commands.Responses;

public class MovimentarContaResult
{
    public Guid IdMovimento { get; set; }
    public Guid IdMovimentoNovo { get; }

    public MovimentarContaResult(Guid idMovimentoNovo)
        => IdMovimentoNovo = idMovimentoNovo;
}
