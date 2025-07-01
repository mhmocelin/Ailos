using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;

namespace Questao5.Application.Services;

public interface IMovimentarContaService
{
    Task<MovimentarContaResult> MovimentarAsync(MovimentarContaCommand command);
}
