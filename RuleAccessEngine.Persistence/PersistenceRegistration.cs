using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RuleAccessEngine.Domain.Repositories;
using RuleAccessEngine.Persistence.Repositories;

namespace RuleAccessEngine.Persistence
{
    public static class PersistenceRegistration
    {
        public static IServiceCollection ConfigurePersistenceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IRuleAccessDBContext,RuleAccessDBContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SqlServer")));


           services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
           services.AddScoped<IRuleRepository, RuleRepository>();

            return services;
        }
    }
}
