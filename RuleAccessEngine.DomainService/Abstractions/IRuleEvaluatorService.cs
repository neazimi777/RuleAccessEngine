using RuleAccessEngine.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleAccessEngine.DomainService.Abstractions
{
    public interface IRuleEvaluatorService
    {
        public AccessDecision Evaluate(RuleDto rule, AccessRequestDto request);
    }
}
