using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RuleAccessEngine.DomainService.Abstractions;
using RuleAccessEngine.DomainService.RuleEvaluator;
using RuleAccessEngine.DomainService.RuleEvaluator.EvaluatorStrategy;
using System.Reflection;

namespace RuleAccessEngine.DomainService
{
    public static class ServiceRegisteration
    {
        public static IServiceCollection ConfigureServiceRegisteration(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddAutoMapper(assembly);
           
            services.AddScoped<IRuleEvaluatorFactory, RuleEvaluatorFactory>();
            services.AddScoped<IRuleEvaluatorService, RuleEvaluatorService>();
            services.AddScoped<ExpressionRuleEvaluator>();

            return services;
        }
    }
}
