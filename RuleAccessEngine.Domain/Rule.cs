using RuleAccessEngine.Domain.Enums;

namespace RuleAccessEngine.Domain
{
    public class Rule
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Condition { get; set; } = string.Empty;
        public RuleType Type { get; set; } = RuleType.Expression;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    }
}
