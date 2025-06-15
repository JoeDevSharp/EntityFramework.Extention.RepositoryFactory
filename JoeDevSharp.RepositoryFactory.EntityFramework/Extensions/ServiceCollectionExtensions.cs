using JoeDevSharp.RepositoryFactory.EntityFramework.Core;
using JoeDevSharp.RepositoryFactory.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JoeDevSharp.RepositoryFactory.EntityFramework.Extensions
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
