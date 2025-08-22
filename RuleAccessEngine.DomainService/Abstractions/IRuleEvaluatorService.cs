using RuleAccessEngine.Dto;

namespace RuleAccessEngine.DomainService.Abstractions
{
    public interface IRuleEvaluatorService
    {
        public EvaluateResponseDto Evaluate(EvaluateRequestDto evaluateRequestDto);
    }
}
