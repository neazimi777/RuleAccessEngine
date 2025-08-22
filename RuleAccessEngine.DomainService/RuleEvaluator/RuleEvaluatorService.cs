using RuleAccessEngine.Domain.Repositories;
using RuleAccessEngine.DomainService.Abstractions;
using RuleAccessEngine.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleAccessEngine.DomainService.RuleEvaluator
{
    public class RuleEvaluatorService : IRuleEvaluatorService
    {

        private readonly IRuleEvaluatorFactory _ruleEvaluatorFactory;
        private readonly IRuleRepository _ruleRepository;

        public RuleEvaluatorService(IRuleEvaluatorFactory ruleEvaluatorFactory,IRuleRepository ruleRepository)
        {
            _ruleEvaluatorFactory = ruleEvaluatorFactory;
            _ruleRepository = ruleRepository;
        }

        public EvaluateResponseDto Evaluate(EvaluateRequestDto evaluateRequestDto)
        {
            _ruleRepository.GetAsync()

            if (evaluateRequestDto is null) return new EvaluateResponseDto(false, "Invalid payload.");

            if (evaluateRequestDto.Rule == null)
                return new EvaluateResponseDto(false, "No rules provided.");

            //todo : impelement repository 

            var evaluateStrategy = _ruleEvaluatorFactory.Create(evaluateRequestDto.Rule);
            var result = evaluateStrategy.Evaluate(evaluateRequestDto);

            return new EvaluateResponseDto(result.Allowed,result.Reason,result.Error);
        }
    }
}
