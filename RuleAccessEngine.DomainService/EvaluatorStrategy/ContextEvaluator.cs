using RuleAccessEngine.DomainService.Abstractions;

namespace RuleAccessEngine.DomainService.EvaluatorStrategy
{
    public class ContextEvaluator
    {
        private readonly IRuleEvaluator _ruleEvaluator;
        public ContextEvaluator(IRuleEvaluator ruleEvaluator)
        {
            _ruleEvaluator = ruleEvaluator;
        }


    }
}
