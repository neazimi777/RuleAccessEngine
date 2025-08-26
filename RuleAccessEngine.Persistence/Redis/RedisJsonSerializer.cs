using System.Text.Json;

namespace RuleAccessEngine.Persistence.Redis
{
    internal static class RedisJsonSerializer
    {
        public static string Serialize<T>(T value, JsonSerializerOptions options) =>
            JsonSerializer.Serialize(value, options);

        public static T? Deserialize<T>(string json, JsonSerializerOptions options) =>
            JsonSerializer.Deserialize<T>(json, options);
    }
}
