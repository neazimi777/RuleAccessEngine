using Microsoft.Extensions.Options;
using RuleAccessEngine.Domain.Repositories;
using RuleAccessEngine.Persistence.Redis;
using StackExchange.Redis;
using System.Text.Json;

namespace RuleAccessEngine.Persistence.Repositories
{
    public class GenericCacheRepository<T> : IGenericCacheRepository<T> where T : class
    {
        private readonly IDatabase _db;
        private readonly RedisCacheOptions _options;

        public GenericCacheRepository(IConnectionMultiplexer mux, RedisCacheOptions options)
        {
            _db = mux.GetDatabase();
            _options = options;
        }

        private string Namespaced(string key) => string.Concat(_options.KeyPrefix, key);

        public async Task<T?> GetAsync(string key, CancellationToken ct = default)
        {
            var val = await _db.StringGetAsync(Namespaced(key)).ConfigureAwait(false);
            return RedisJsonSerializer.Deserialize<T>(val!, _options.JsonOptions);
        }

        public async Task SetAsync(string key, T value, TimeSpan? ttl = null, CancellationToken ct = default)
        {
            var json = RedisJsonSerializer.Serialize(value, _options.JsonOptions);
            var expiry = ttl ?? _options.DefaultTtl;
            await _db.StringSetAsync(Namespaced(key), json, expiry).ConfigureAwait(false);
        }

        public Task RemoveAsync(string key, CancellationToken ct = default) =>
            _db.KeyDeleteAsync(Namespaced(key));

        public async Task<bool> ExistsAsync(string key, CancellationToken ct = default)
        {
            return await _db.KeyExistsAsync(Namespaced(key)).ConfigureAwait(false);
        }

        public async Task<T?> GetOrSetAsync(string key,Func<CancellationToken, Task<T?>> factory,TimeSpan? ttl = null,bool cacheNull = false,CancellationToken ct = default)
        {
            var cached = await GetAsync(key, ct).ConfigureAwait(false);
            if (cached is not null) return cached;

            var value = await factory(ct).ConfigureAwait(false);

            if (value is null && !cacheNull)
                return null;

            await SetAsync(key, value!, ttl, ct).ConfigureAwait(false);
            return value;
        }
    }
}
