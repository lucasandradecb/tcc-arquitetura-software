﻿using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Gsl.Gestao.Estrategica.Domain.Repositories;
using Gsl.Gestao.Estrategica.Infrastructure.Repositories;

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
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IDepositoRepository, DepositoRepository>();
            services.AddScoped<IMercadoriaRepository, MercadoriaRepository>();
            services.AddScoped<ISqlServerDbContext, SqlServerDbContext>();

            return services;
        }
    }
}
