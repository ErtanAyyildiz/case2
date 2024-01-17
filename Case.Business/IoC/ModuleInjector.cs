using Case.Business.Abstracts;
using Case.Business.Concretes;
using Microsoft.Extensions.DependencyInjection;

namespace Case.Business.IoC
{
    public static class ModuleInjector
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();
            return services;
        }
    }
}
