using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace FinancasAPI.Swagger
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, string Title, string Version)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = Title, 
                    Version = Version,
                    Description = Title,
                    TermsOfService = null,
                    Contact = new OpenApiContact { Name = "Desenvolvedor X", Email = "email@Teste.com" }
                });                
            });


            return services;
        }
    }
}
