using RuleAccessEngine.Domain.Enums;
using RuleAccessEngine.Dto;

namespace RuleAccessEngine.DomainService.Abstractions
{
    public interface IRuleEvaluatorFactory
    {
        public IRuleEvaluatorService Create(RuleType ruleType);
    }
}
