using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Gsl.Gestao.Estrategica.Application.Interfaces;

namespace Gsl.Gestao.Estrategica.Application
{
    /// <summary>
    /// Setup da Application
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class Setup
    {
        /// <summary>
        /// Serviços de Domínio da Application
        /// </summary>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IClienteApplication, ClienteApplication>();
            services.AddScoped<IFornecedorApplication, FornecedorApplication>();
            services.AddScoped<IDepositoApplication, DepositoApplication>();
            services.AddScoped<IMercadoriaApplication, MercadoriaApplication>();

            return services;
        }
    }
}
