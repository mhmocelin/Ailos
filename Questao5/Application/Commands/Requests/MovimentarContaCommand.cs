using Questao5.Application.Commands.Responses;
using Questao5.Application.Mediator.Interfaces;

namespace Questao5.Application.Commands.Requests;

public class MovimentarContaCommand : IRequest<MovimentarContaResult>
{
    public Guid ChaveIdempotencia { get; set; }
    public Guid IdContaCorrente { get; set; }
    public decimal Valor { get; set; }
    public char TipoMovimento { get; set; }  // 'C' ou 'D'
}
