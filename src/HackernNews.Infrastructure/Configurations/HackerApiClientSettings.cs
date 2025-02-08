namespace HackernNews.Infrastructure.Configurations
{

    /// <summary>
    /// Configuration settings for the Hacker API client.
    /// </summary>
    public class HackerApiClientSettings
    {
        /// <summary>
        /// The name of the settings section.
        /// </summary>
        public const string Name = "HackerApiClientSettings";

        /// <summary>
        /// The base URL of the Hacker API.
        /// </summary>
        public string BaseUrl { get; set; }
    }

}
