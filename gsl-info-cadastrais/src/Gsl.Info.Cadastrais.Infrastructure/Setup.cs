using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Gsl.Info.Cadastrais.Domain.Repositories;
using Gsl.Info.Cadastrais.Infrastructure.Repositories;

namespace Gsl.Info.Cadastrais.Infrastructure
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
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IDepositoRepository, DepositoRepository>();
            services.AddScoped<IMercadoriaRepository, MercadoriaRepository>();

            services.AddScoped<IMarcaVeiculoRepository, MarcaVeiculoRepository>();
            services.AddScoped<IModeloVeiculoRepository, ModeloVeiculoRepository>();
            services.AddScoped<IOperadorRepository, OperadorRepository>();
            services.AddScoped<IVeiculoRepository, VeiculoRepository>();
            services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            return services;
        }
    }
}
