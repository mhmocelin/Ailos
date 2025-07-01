using FluentValidation;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Handlers;
using Questao5.Application.Mediator;
using Questao5.Application.Mediator.Interfaces;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Application.Services;
using Questao5.Application.Validators;
using Questao5.Domain.Services;

namespace Questao5.Infrastructure.DependencyInjection;

public static class ApplicationDependencies
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IApplicationDispatcher, ApplicationDispatcher>();
        services.AddScoped<IMovimentarContaService, MovimentarContaService>();
        services.AddScoped<IValidator<MovimentarContaCommand>, MovimentarContaCommandValidator>();
        services.AddScoped<IRequestHandler<MovimentarContaCommand, MovimentarContaResult>, MovimentarContaHandler>();
        services.AddScoped<IRequestHandler<SaldoContaCorrenteQuery, SaldoContaCorrenteResult>, SaldoContaCorrenteHandler>();

        return services;
    }
}
