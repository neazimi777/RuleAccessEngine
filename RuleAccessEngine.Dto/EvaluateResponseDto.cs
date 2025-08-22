namespace RuleAccessEngine.Dto
{
    public record EvaluateResponseDto(bool Allowed, string? Reason = null, string? Error = null);
}
