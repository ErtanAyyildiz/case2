using Microsoft.Extensions.DependencyInjection;
using Case.DataAccess.Repositories.IRepositories;
using Case.DataAccess.Repositories;

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
