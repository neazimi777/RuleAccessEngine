namespace RuleAccessEngine.Dto
{
    public record AccessDecision(bool Allowed, string? Reason = null, string? Error = null);
}
