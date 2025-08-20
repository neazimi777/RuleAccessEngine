using RuleAccessEngine.Dto;

namespace RuleAccessEngine.DomainService.Abstractions
{
    public interface IRuleEvaluatorFactory
    {
        IRuleEvaluator Create(RuleDto rule);
    }
}
