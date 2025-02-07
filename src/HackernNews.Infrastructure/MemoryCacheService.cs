using HackernNews.Core.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace HackernNews.Infrastructure
{

    /// <summary>
    /// Service for handling memory caching operations.
    /// </summary>
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _defaultSlidingExpiration;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryCacheService"/> class.
        /// </summary>
        /// <param name="cache">The memory cache instance.</param>
        /// <param name="configuration">The configuration instance.</param>
        public MemoryCacheService(IMemoryCache cache, IConfiguration configuration)
        {
            _cache = cache;

            // Get default sliding expiration from appsettings.json
            _defaultSlidingExpiration = TimeSpan.FromMinutes(
                configuration.GetValue<int>("CacheSettings:SlidingExpirationMinutes", 10)
            );
        }

        /// <inheritdoc />
        public T Get<T>(string key, Func<T> fetch, TimeSpan? absoluteExpiration = null)
        {
            if (_cache.TryGetValue(key, out T value) && value != null)
            {
                return value;
            }

            // Fetch data using provided function
            value = fetch();

            // Cache the fetched value with sliding expiration
            var cacheOptions = new MemoryCacheEntryOptions
            {
                SlidingExpiration = _defaultSlidingExpiration
            };

            if (absoluteExpiration.HasValue)
            {
                cacheOptions.AbsoluteExpirationRelativeToNow = absoluteExpiration;
            }

            _cache.Set(key, value, cacheOptions);

            return value;
        }

        /// <inheritdoc />
        public void Set<T>(string key, T value, TimeSpan expiration)
        {
            _cache.Set(key, value, new MemoryCacheEntryOptions
            {
                SlidingExpiration = expiration
            });
        }

        /// <inheritdoc />
        public bool TryGetValue<T>(string key, out T value)
        {
            return _cache.TryGetValue(key, out value);
        }

        /// <inheritdoc />
        public void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}
