namespace HackernNews.Core.Interfaces
{
    /// <summary>
    /// Interface for cache service to handle caching operations.
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// Gets the cached value for the specified key. If the key does not exist, fetches the value using the provided function and caches it.
        /// </summary>
        /// <typeparam name="T">The type of the value to get.</typeparam>
        /// <param name="key">The cache key.</param>
        /// <param name="fetch">The function to fetch the value if it is not found in the cache.</param>
        /// <param name="absoluteExpiration">The absolute expiration time for the cache entry.</param>
        /// <returns>The cached value.</returns>
        public T Get<T>(string key, Func<T> fetch, TimeSpan? absoluteExpiration = null);

        /// <summary>
        /// Sets the specified value in the cache with the given key and expiration time.
        /// </summary>
        /// <typeparam name="T">The type of the value to set.</typeparam>
        /// <param name="key">The cache key.</param>
        /// <param name="value">The value to cache.</param>
        public void Set<T>(string key, T value);

        /// <summary>
        /// Tries to get the cached value for the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the value to get.</typeparam>
        /// <param name="key">The cache key.</param>
        /// <param name="value">The cached value if found.</param>
        /// <returns>True if the value was found in the cache; otherwise, false.</returns>
        public bool TryGetValue<T>(string key, out T value);

        /// <summary>
        /// Removes the cached value for the specified key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        public void Remove(string key);
    }
}
