using Domain.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.GatewayPagamento
{
    public static class DependenceInjection
    {
        public static IServiceCollection AddGatewayPagamento(this IServiceCollection services)
        {
            services.AddScoped<IPagamentoExternoGateway, FakeGatewayPagamento>();

            return services;
        }
    }
}
