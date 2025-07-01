using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Exceptions;

namespace Questao5.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContaCorrenteController : BaseController
{
    private readonly IMediator _mediator;

    public ContaCorrenteController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("movimentar")]
    [ProducesResponseType(typeof(MovimentarContaResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> MovimentarConta([FromBody] MovimentarContaCommand command)
        => Response(await _mediator.Send(command));
       

    [HttpGet("saldo/{id}")]
    [ProducesResponseType(typeof(SaldoContaCorrenteResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ConsultarSaldo([FromRoute] Guid id)
        => Response(await _mediator.Send(new SaldoContaCorrenteQuery(id)));    
}