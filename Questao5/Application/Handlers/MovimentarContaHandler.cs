using Dapper;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Mediator.Interfaces;
using Questao5.Application.Services;
using Questao5.Domain.Exceptions;
using System.Data;

namespace Questao5.Application.Handlers;

public class MovimentarContaHandler : IRequestHandler<MovimentarContaCommand, MovimentarContaResult>
{
    private readonly IMovimentarContaService _service;

    public MovimentarContaHandler(IMovimentarContaService service)
    {
        _service = service;
    }

    public async Task<MovimentarContaResult> Handle(MovimentarContaCommand request, CancellationToken cancellationToken)
        => await _service.MovimentarAsync(request);
}
