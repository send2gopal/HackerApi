namespace HackernNews.Infrastructure.Configurations
{

    /// <summary>
    /// Configuration settings for caching.
    /// </summary>
    public class CacheSettings
    {
        /// <summary>
        /// The name of the cache settings section.
        /// </summary>
        public const string Name = "CacheSettings";

        /// <summary>
        /// The sliding expiration time in minutes.
        /// </summary>
        public int SlidingExpirationMinutes { get; set; }
    }

}
