namespace RuleAccessEngine.Dto
{
    public class EvaluateRequestDto
    {
        public AccessRequestDto Request { get; init; } = new();
        public RuleDto Rule { get; init; } = new();
    }
}
