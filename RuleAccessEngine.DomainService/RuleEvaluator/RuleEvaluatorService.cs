using AutoMapper;
using RuleAccessEngine.Domain.Repositories;
using RuleAccessEngine.DomainService.Abstractions;
using RuleAccessEngine.Dto;

namespace RuleAccessEngine.DomainService.RuleEvaluator
{
    public class RuleEvaluatorService : IRuleEvaluatorService
    {

        private readonly IRuleEvaluatorFactory _ruleEvaluatorFactory;
        private readonly IRuleRepository _ruleRepository;
        private readonly IMapper _mapper;

        public RuleEvaluatorService(IRuleEvaluatorFactory ruleEvaluatorFactory, IRuleRepository ruleRepository, IMapper mapper)
        {
            _ruleEvaluatorFactory = ruleEvaluatorFactory;
            _ruleRepository = ruleRepository;
            _mapper = mapper;
        }

        public async Task<EvaluateResponseDto> Evaluate(EvaluateRequestDto evaluateRequestDto)
        {
            if (evaluateRequestDto is null) return new EvaluateResponseDto(false, "Invalid payload.");

            var evaluateStrategy = _ruleEvaluatorFactory.Create(evaluateRequestDto.RuleType);
            var result = await evaluateStrategy.Evaluate(evaluateRequestDto);

            return new EvaluateResponseDto(result.Allowed, result.Reason, result.Error);
        }
    }
}
