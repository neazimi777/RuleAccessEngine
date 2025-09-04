using RuleAccessEngine.Domain;
using RuleAccessEngine.Domain.Repositories;
using RuleAccessEngine.Persistence.Redis;
using StackExchange.Redis;

namespace RuleAccessEngine.Persistence.Repositories
{
    public class EvaluateCacheRepository : GenericCacheRepository<EvaluateResult>, IEvaluateCacheRepository
    {
        public EvaluateCacheRepository(IConnectionMultiplexer mux, RedisCacheOptions options) : base(mux, options)
        {
        }
        public string BuildEvaluateCacheKey(dynamic rule, object request)
        {
            var version = rule?.UpdatedAt is DateTime dt && dt != default
                ? dt.Ticks.ToString()
                : Sha256(rule?.Condition ?? string.Empty);

            var reqJson = System.Text.Json.JsonSerializer.Serialize(request, new System.Text.Json.JsonSerializerOptions
            {
                PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            });
            var reqHash = Sha256(reqJson);

            return $"rule_eval:{rule.Id}:{version}:{reqHash}";
        }

        public static string Sha256(string s)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();
            return Convert.ToHexString(sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(s)));
        }
    }
}
