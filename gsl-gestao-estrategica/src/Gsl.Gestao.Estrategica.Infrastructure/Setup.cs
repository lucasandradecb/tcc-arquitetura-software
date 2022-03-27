using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Gsl.Gestao.Estrategica.Domain.Repositories;
using Gsl.Gestao.Estrategica.Infrastructure.Repositories;
using Gsl.Gestao.Estrategica.Infrastructure.Gateways.Interfaces;
using Gsl.Gestao.Estrategica.Infrastructure.Gateways;
using System;

namespace Gsl.Gestao.Estrategica.Infrastructure
{
    /// <summary>
    /// Setup Infrastructure
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class Setup
    {
        /// <summary>
        /// Serviços de Domínio da Infrastructure
        /// </summary>
        public static IServiceCollection AddInfraServices(this IServiceCollection services)
        {
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IEstoqueRepository, EstoqueRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IEntregaRepository, EntregaRepository>();
            services.AddScoped<ISqlServerDbContext, SqlServerDbContext>();
            services.AddScoped<IGslInfoCadastraisGateway, GslInfoCadastraisGateway>();

            AdicionarGslInfoCadastraisGateway(services);

            return services;
        }

        /// <summary>
        /// Adiciona  o GslInfoCadastraisGateway à configuração de serviços
        /// </summary>
        /// <param name="services"></param>
        private static void AdicionarGslInfoCadastraisGateway(IServiceCollection services)
        {
            services.AddHttpClient<IGslInfoCadastraisGateway, GslInfoCadastraisGateway>(client =>
            {
                client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("URL_GSL_INFO_CADASTRAIS"));
            });
        }
    }
}
