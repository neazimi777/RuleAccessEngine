using AutoMapper;
using DynamicExpresso;
using RuleAccessEngine.Domain;
using RuleAccessEngine.Domain.Repositories;
using RuleAccessEngine.DomainService.Abstractions;
using RuleAccessEngine.Dto;

namespace RuleAccessEngine.DomainService.RuleEvaluator.EvaluatorStrategy
{
    public class ExpressionRuleEvaluator : IRuleEvaluatorService
    {
        private readonly IRuleRepository _ruleRepository;
        private readonly IRuleCacheRepository _ruleCache;
        private readonly IEvaluateCacheRepository _evaluateCache;
        private readonly IMapper _mapper;

        private static readonly TimeSpan RuleTtl = TimeSpan.FromMinutes(10);
        private static readonly TimeSpan EvalTtl = TimeSpan.FromSeconds(60);
        public ExpressionRuleEvaluator(IRuleRepository ruleRepository, IRuleCacheRepository ruleCacheRepository, IEvaluateCacheRepository evaluateCacheRepository, IMapper mapper)
        {
            _ruleRepository = ruleRepository;
            _ruleCache = ruleCacheRepository;
            _evaluateCache = evaluateCacheRepository;
            _mapper = mapper;
        }
        public async Task<EvaluateResponseDto> Evaluate(EvaluateRequestDto evaluateRequestDto)
        {

            var ruleKey = $"rule:{evaluateRequestDto.RuleID}";
            var rule = await _ruleCache.GetOrSetAsync(
                ruleKey,
                async _ => await _ruleRepository.GetAsync(evaluateRequestDto.RuleID),
                ttl: RuleTtl,
                cacheNull: false
            );

            if (rule is null)
                return new(false, "No rules provided.");

            if (!rule.IsActive)
                return new(false, Reason: $"Rule '{rule.Name}' is inactive.");

            if (string.IsNullOrWhiteSpace(rule.Condition))
                return new(false, Error: $"Rule '{rule.Name}' has empty condition.");


            var evalKey = _evaluateCache.BuildEvaluateCacheKey(rule, evaluateRequestDto.Request);
            var cached = await _evaluateCache.GetAsync(evalKey);
            if (cached is not null)
                return _mapper.Map<EvaluateResponseDto>(cached);

            try
            {
                var interpreter = new Interpreter();
                interpreter.SetVariable("req", evaluateRequestDto.Request);

                var ok = interpreter.Eval<bool>(rule.Condition);

                var response = ok
                    ? new EvaluateResponseDto(true, Reason: $"Rule '{rule.Name}' matched.")
                    : new EvaluateResponseDto(false, Reason: $"Rule '{rule.Name}' not matched.");


                await _evaluateCache.SetAsync(evalKey, _mapper.Map<EvaluateResult>(response), EvalTtl);
                return response;
            }
            catch (Exception ex)
            {
                return new EvaluateResponseDto(false, Error: $"Evaluation error in rule '{rule.Name}': {ex.Message}");
            }
        }

    }
}
