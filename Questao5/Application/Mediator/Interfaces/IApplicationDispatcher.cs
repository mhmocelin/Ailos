namespace Questao5.Application.Mediator.Interfaces;

public interface IApplicationDispatcher
{
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
}
