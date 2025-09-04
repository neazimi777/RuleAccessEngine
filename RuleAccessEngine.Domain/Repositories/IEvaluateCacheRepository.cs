namespace RuleAccessEngine.Domain.Repositories
{
    public interface IEvaluateCacheRepository : IGenericCacheRepository<EvaluateResult>
    {
        public string BuildEvaluateCacheKey(dynamic rule, object request);
    }
}
