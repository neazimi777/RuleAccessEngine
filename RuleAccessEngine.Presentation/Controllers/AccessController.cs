using Microsoft.AspNetCore.Mvc;
using RuleAccessEngine.DomainService.Abstractions;
using RuleAccessEngine.Dto;
using RuleAccessEngine.Presentation;

namespace RuleAccessEngine.Controllers
{
    public class AccessController : ApiControllerBase
    {
        private readonly IRuleEvaluatorService _ruleEvaluatorService;

        public AccessController(IRuleEvaluatorService ruleEvaluatorService)
        {
            _ruleEvaluatorService = ruleEvaluatorService;
        }

        [HttpPost("Evaluate")]
        public ActionResult<EvaluateResponseDto> Evaluate([FromBody] EvaluateRequestDto input)
        {
            return Ok(_ruleEvaluatorService.Evaluate(input));
        }
    }
}
