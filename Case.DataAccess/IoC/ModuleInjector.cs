using Case.DataAccess.Repositories;
using Case.DataAccess.Repositories.IRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace Case.DataAccess.IoC
{
    public static class ModuleInjector
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


            return services;
        }
    }
}
