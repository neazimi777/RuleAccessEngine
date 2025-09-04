namespace RuleAccessEngine.Domain
{
    public class EvaluateResult
    {
        public bool Allowed { get; set; }
        public string? Reason { get; set; }
        public string? Error { get; set; }
    }
}
