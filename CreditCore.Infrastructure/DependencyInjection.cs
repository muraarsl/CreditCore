
using CreditCore.Infrastructure.Interfaces;
using CreditCore.Infrastructure.Persistence;
using CreditCore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace CreditCore.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<CreditDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("CreditDb")));

            
            services.AddScoped <ICreditRepository, CreditRepository>();
            return services;
        }
    }
}
