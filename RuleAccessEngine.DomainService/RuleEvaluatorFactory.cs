using RuleAccessEngine.DomainService.Abstractions;
using RuleAccessEngine.Dto;

namespace RuleAccessEngine.DomainService
{
    public class RuleEvaluatorFactory : IRuleEvaluatorFactory
    {
        private readonly IRuleEvaluator _ruleEvaluatorService;

        public RuleEvaluatorFactory(IRuleEvaluator ruleEvaluatorService)
        {
            _ruleEvaluatorService = ruleEvaluatorService;
        }
        public IRuleEvaluator Create(RuleDto rule)
        {
            


        }
    }
}
