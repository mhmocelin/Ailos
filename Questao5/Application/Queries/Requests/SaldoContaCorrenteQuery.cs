using MediatR;
using Questao5.Application.Queries.Responses;

namespace Questao5.Application.Queries.Requests;

public class SaldoContaCorrenteQuery : IRequest<SaldoContaCorrenteResult>
{
    public Guid IdContaCorrente { get; set; }

    public SaldoContaCorrenteQuery(Guid idContaCorrente) => IdContaCorrente = idContaCorrente;
}