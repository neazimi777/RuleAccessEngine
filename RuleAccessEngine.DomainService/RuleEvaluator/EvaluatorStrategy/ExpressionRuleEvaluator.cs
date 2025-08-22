using DynamicExpresso;
using RuleAccessEngine.DomainService.Abstractions;
using RuleAccessEngine.Dto;
using System.Data;

namespace RuleAccessEngine.DomainService.RuleEvaluator.EvaluatorStrategy
{
    public class ExpressionRuleEvaluator : IRuleEvaluatorService
    {
        public EvaluateResponseDto Evaluate(EvaluateRequestDto evaluateRequestDto)
        {
            if (!evaluateRequestDto.Rule.IsActive)
                return new(false, Reason: $"Rule '{evaluateRequestDto.Rule.Name}' is inactive.");

            if (string.IsNullOrWhiteSpace(evaluateRequestDto.Rule.Condition))
                return new(false, Error: $"Rule '{evaluateRequestDto.Rule.Name}' has empty condition.");

            try
            {
                var interpreter = new Interpreter();

                interpreter.SetVariable("req", evaluateRequestDto.Request);

                var ok = interpreter.Eval<bool>(evaluateRequestDto.Rule.Condition);

                return ok
                    ? new(true, Reason: $"Rule '{evaluateRequestDto.Rule.Name}' matched.")
                    : new(false, Reason: $"Rule '{evaluateRequestDto.Rule.Name}' not matched.");
            }
            catch (Exception ex)
            {
                return new(false, Error: $"Evaluation error in rule '{evaluateRequestDto.Rule.Name}': {ex.Message}");
            }

        }
    }
}
