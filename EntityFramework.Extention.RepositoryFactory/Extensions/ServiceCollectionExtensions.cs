using EntityFramework.Extention.RepositoryFactory.Core;
using EntityFramework.Extention.RepositoryFactory.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.Extention.RepositoryFactory.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositoryFactory<TContext>(this IServiceCollection services)
            where TContext : DbContext, new()
        {
            services.AddScoped<RepositoryFactory<TContext>>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }
    }
}
