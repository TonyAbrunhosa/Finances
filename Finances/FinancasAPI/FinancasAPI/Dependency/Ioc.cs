using FinancasAPI.Application.Service;
using FinancasAPI.Domain.Contracts.IRepository;
using FinancasAPI.Domain.Contracts.IService;
using FinancasAPI.Infra.Base;
using FinancasAPI.Infra.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace FinancasAPI.Dependency
{
    public static class Ioc
    {
        public static IServiceCollection ResolveDependency(this IServiceCollection services)
        {
            services.AddScoped<CommunicationJson, CommunicationJson>();

            services.AddTransient<IOperationsRepository, OperationsRepository>();
            services.AddTransient<IOperationsService, OperationsService>();

            return services;
        }
    }
}
