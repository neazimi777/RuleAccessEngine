using System.Text.Json;

namespace RuleAccessEngine.Persistence.Redis
{
    public class RedisCacheOptions
    {
        public string KeyPrefix { get; set; } = "app:";
        public TimeSpan? DefaultTtl { get; set; } = TimeSpan.FromMinutes(10);
        public JsonSerializerOptions JsonOptions { get; set; } = new(JsonSerializerDefaults.Web)
        {
            WriteIndented = false,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };
    }
}
