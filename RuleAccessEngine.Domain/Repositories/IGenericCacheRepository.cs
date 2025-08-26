namespace RuleAccessEngine.Domain.Repositories
{
    public interface IGenericCacheRepository<T>
    {
        Task<T?> GetAsync(string key, CancellationToken ct = default);
        Task SetAsync(string key, T value, TimeSpan? ttl = null, CancellationToken ct = default);
        Task RemoveAsync(string key, CancellationToken ct = default);
        Task<bool> ExistsAsync(string key, CancellationToken ct = default);
        Task<T?> GetOrSetAsync(
            string key,
            Func<CancellationToken, Task<T?>> factory,
            TimeSpan? ttl = null,
            bool cacheNull = false,
            CancellationToken ct = default);
    }
}

   

