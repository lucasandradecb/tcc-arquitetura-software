using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Gsl.Gestao.Estrategica.Api.Filters;
using Gsl.Gestao.Estrategica.Api.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Gsl.Gestao.Estrategica.Infrastructure;
using Gsl.Gestao.Estrategica.Application;
using Gsl.Gestao.Estrategica.Domain.Repositories;
using Gsl.Gestao.Estrategica.Infrastructure.Repositories;
using System.Text.Json.Serialization;

namespace Gsl.Gestao.Estrategica.Api
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
                    Title = "Gsl.Gestao.Estrategica",
                    Description = "API - Gsl.Gestao.Estrategica",
                    Version = "v1"
                });

                var apiPath = Path.Combine(AppContext.BaseDirectory, "Gsl.Gestao.Estrategica.Api.xml");
                var applicationPath = Path.Combine(AppContext.BaseDirectory, "Gsl.Gestao.Estrategica.Application.xml");

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

            app.UsePathBase("/Gsl.Gestao.Estrategica");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/Gsl.Gestao.Estrategica/swagger/v1/swagger.json", "API Gsl.Gestao.Estrategica");
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