using HackernNews.Core.Interfaces;
using HackernNews.Infrastructure.Configurations;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HackernNews.Infrastructure
{

    /// <summary>
    /// Service for handling memory caching operations.
    /// </summary>
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private readonly CacheSettings _cacheSettings;
        private readonly ILogger<MemoryCacheService> _logger;
        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryCacheService"/> class.
        /// </summary>
        /// <param name="cache">The memory cache instance.</param>
        /// <param name="options">The cache settings options.</param>
        /// <param name="logger">The logger instance.</param>
        public MemoryCacheService(IMemoryCache cache, IOptions<CacheSettings> cacheSettings, ILogger<MemoryCacheService> logger)
        {
            _cache = cache;
            _logger = logger;
            // Get default sliding expiration from appsettings.json
            _cacheSettings = cacheSettings.Value?? throw new ArgumentException("Cache settings are required.");
        }

        /// <inheritdoc />
        public T Get<T>(string key, Func<T> fetch, TimeSpan? absoluteExpiration = null)
        {
            if (_cache.TryGetValue(key, out T value) && value != null)
            {
                _logger.LogInformation($"Cache hit for key: {key}");
                return value;
            }
            _logger.LogInformation($"Cache miss for key: {key}");
            // Fetch data using provided function
            value = fetch();

            // Cache the fetched value with sliding expiration
            var cacheOptions = new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(_cacheSettings.SlidingExpirationMinutes)
            };

            if (absoluteExpiration.HasValue)
            {
                cacheOptions.AbsoluteExpirationRelativeToNow = absoluteExpiration;
            }

            _cache.Set(key, value, cacheOptions);

            return value;
        }

        /// <inheritdoc />
        public void Set<T>(string key, T value)
        {
            _cache.Set(key, value, new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(_cacheSettings.SlidingExpirationMinutes)
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
