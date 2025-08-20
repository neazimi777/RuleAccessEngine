using DynamicExpresso;
using RuleAccessEngine.DomainService.Abstractions;
using RuleAccessEngine.Dto;
using System.Data;

namespace RuleAccessEngine.DomainService.EvaluatorStrategy
{
    public class ExpressionRuleEvaluator : IRuleEvaluator
    {
        public AccessDecision Evaluate(RuleDto rule, AccessRequestDto request)
        {
            if (!rule.IsActive)
                return new(false, Reason: $"Rule '{rule.Name}' is inactive.");

            if (string.IsNullOrWhiteSpace(rule.Condition))
                return new(false, Error: $"Rule '{rule.Name}' has empty condition.");

            try
            {
                var interpreter = new Interpreter();

                interpreter.SetVariable("req", request);

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
