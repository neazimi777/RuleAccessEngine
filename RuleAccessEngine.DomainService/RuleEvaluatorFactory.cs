using Microsoft.Extensions.DependencyInjection;
using RuleAccessEngine.Domain.Enums;
using RuleAccessEngine.DomainService.Abstractions;
using RuleAccessEngine.DomainService.EvaluatorStrategy;
using RuleAccessEngine.Dto;

namespace RuleAccessEngine.DomainService
{
    public class RuleEvaluatorFactory : IRuleEvaluatorFactory
    {
        private readonly IServiceProvider _sp;
        public RuleEvaluatorFactory(IServiceProvider sp) => _sp = sp;

        public IRuleEvaluatorService Create(RuleDto rule)
        {
            return rule.Type switch
            {
                RuleType.Expression => _sp.GetRequiredService<ExpressionRuleEvaluator>(),
                //RuleType.Specification => _sp.GetRequiredService<SpecificationRuleEvaluatorService>(),
                //RuleType.External => _sp.GetRequiredService<ExternalServiceRuleEvaluatorService>(),
                _ => throw new NotSupportedException($"Unsupported rule type: {rule.Type}")
            };

        }
    }
}
