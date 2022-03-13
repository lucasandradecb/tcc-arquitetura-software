using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Gsl.Info.Cadastrais.Application.Interfaces;

namespace Gsl.Info.Cadastrais.Application
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

            services.AddScoped<IUsuarioApplication, UsuarioApplication>();
            services.AddScoped<IVeiculoApplication, VeiculoApplication>();
            return services;
        }
    }
}
