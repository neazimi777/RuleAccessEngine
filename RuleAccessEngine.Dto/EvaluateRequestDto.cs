using RuleAccessEngine.Domain.Enums;

namespace RuleAccessEngine.Dto
{
    public class EvaluateRequestDto
    {
        public AccessRequestDto Request { get; init; } = new();
        public Guid RuleID { get; set; }
        public RuleType RuleType { get; set; }
    }
}
