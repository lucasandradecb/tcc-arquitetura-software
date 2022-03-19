using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Gsl.Info.Cadastrais.Api.Filters;
using Gsl.Info.Cadastrais.Api.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Gsl.Info.Cadastrais.Infrastructure;
using Gsl.Info.Cadastrais.Application;
using Gsl.Info.Cadastrais.Domain.Repositories;
using Gsl.Info.Cadastrais.Infrastructure.Repositories;
using System.Text.Json.Serialization;

namespace Gsl.Info.Cadastrais.Api
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddControllers();

            services.AddMvc(options => options.Filters.Add(new DefaultExceptionFilterAttribute()));

            services.AddSingleton<ConnectionString>(new ConnectionString(Configuration.GetValue<string>("ConnectionString:SqlServer_db")));

            services.AddRedis()
                .WithEnviroments(Configuration)
                .Add<IClienteRepository, ClienteRepository>()
                .Add<IFornecedorRepository, FornecedorRepository>()
                .Add<IDepositoRepository, DepositoRepository>();
                //.Add<IMercadoriaRepository, MercadoriaRepository>();

            services.AddAutoMapper(typeof(ClienteApplication));
            services.AddAutoMapper(typeof(FornecedorApplication));
            services.AddAutoMapper(typeof(DepositoApplication));
            services.AddAutoMapper(typeof(MercadoriaApplication));

            services.AddInfraServices();

            services.AddApplicationServices();

            services.AddLoggingSerilog();

            services.AddHealthChecks();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Gsl.Info.Cadastrais",
                    Description = "API - Gsl.Info.Cadastrais",
                    Version = "v1"
                });

                var apiPath = Path.Combine(AppContext.BaseDirectory, "Gsl.Info.Cadastrais.Api.xml");
                var applicationPath = Path.Combine(AppContext.BaseDirectory, "Gsl.Info.Cadastrais.Application.xml");

                c.IncludeXmlComments(apiPath);
                c.IncludeXmlComments(applicationPath);
            });

            services.AddControllersWithViews()
                    .AddJsonOptions(options =>
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UsePathBase("/Gsl.Info.Cadastrais");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/Gsl.Info.Cadastrais/swagger/v1/swagger.json", "API Gsl.Info.Cadastrais");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
