using RuleAccessEngine.Dto;

namespace RuleAccessEngine.DomainService.Abstractions
{
    public interface IRuleEvaluatorFactory
    {
        IRuleEvaluatorService Create(RuleDto rule);
    }
}
