using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Mediator.Interfaces;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;

namespace Questao5.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContaCorrenteController : BaseController
{
    private readonly IApplicationDispatcher _dispatcher;

    public ContaCorrenteController(IApplicationDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpPost("movimentar")]
    [ProducesResponseType(typeof(MovimentarContaResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> MovimentarConta([FromBody] MovimentarContaCommand command)
        => Response(await _dispatcher.Send(command));
       

    [HttpGet("saldo/{id}")]
    [ProducesResponseType(typeof(SaldoContaCorrenteResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ConsultarSaldo(Guid id)
        => Response(await _dispatcher.Send(new SaldoContaCorrenteQuery(id)));    
}