using DynamicExpresso;
using RuleAccessEngine.Domain.Repositories;
using RuleAccessEngine.DomainService.Abstractions;
using RuleAccessEngine.Dto;
using System.Data;

namespace RuleAccessEngine.DomainService.RuleEvaluator.EvaluatorStrategy
{
    public class ExpressionRuleEvaluator : IRuleEvaluatorService
    {
        private readonly IRuleRepository _ruleRepository;
        public ExpressionRuleEvaluator(IRuleRepository ruleRepository)
        {
            _ruleRepository = ruleRepository;
        }
        public async Task<EvaluateResponseDto> Evaluate(EvaluateRequestDto evaluateRequestDto)
        {
            var rule = await _ruleRepository.GetAsync(evaluateRequestDto.RuleID);

            if (rule == null)
                return new EvaluateResponseDto(false, "No rules provided.");

            if (!rule.IsActive)
                return new(false, Reason: $"Rule '{rule.Name}' is inactive.");

            if (string.IsNullOrWhiteSpace(rule.Condition))
                return new(false, Error: $"Rule '{rule.Name}' has empty condition.");

            try
            {
                var interpreter = new Interpreter();

                interpreter.SetVariable("req", evaluateRequestDto.Request);

                var ok = interpreter.Eval<bool>(rule.Condition);

                return ok
                    ? new(true, Reason: $"Rule '{rule.Name}' matched.")
                    : new(false, Reason: $"Rule '{rule.Name}' not matched.");
            }
            catch (Exception ex)
            {
                return new(false, Error: $"Evaluation error in rule '{rule.Name}': {ex.Message}");
            }

        }
    }
}
