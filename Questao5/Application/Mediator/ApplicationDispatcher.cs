using Questao5.Application.Mediator.Interfaces;

namespace Questao5.Application.Mediator
{
    public class ApplicationDispatcher : IApplicationDispatcher
    {
        private readonly IServiceProvider _provider;

        public ApplicationDispatcher(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            var handler = _provider.GetService(handlerType);

            if (handler == null)
                throw new InvalidOperationException($"Handler não encontrado para {request.GetType().Name}");

            var method = handlerType.GetMethod("Handle");
            var task = (Task<TResponse>)method!.Invoke(handler, new object[] { request, cancellationToken })!;

            return await task;
        }
    }
}
