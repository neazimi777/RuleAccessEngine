using RuleAccessEngine.Domain.Enums;

namespace RuleAccessEngine.Dto
{
    public class RuleDto
    {
        public Guid Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string Condition { get; set; } = string.Empty;
        public RuleType Type { get; set; } = RuleType.Expression;
        public bool IsActive { get; set; } = true;
    }
}
