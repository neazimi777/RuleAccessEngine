using RuleAccessEngine.Dto;

namespace RuleAccessEngine.DomainService.Abstractions
{
    public interface IRuleEvaluatorService
    {
        public Task<EvaluateResponseDto> Evaluate(EvaluateRequestDto evaluateRequestDto);
    }
}
