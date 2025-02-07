using System.Text.Json.Serialization;

namespace HackernNews.Infrastructure.HackerNewsSource
{
    /// <summary>
    /// Represents the details of a story from Hacker News.
    /// </summary>
    public class StoryDetailsResponse
    {
        /// <summary>
        /// Gets or sets the author of the story.
        /// </summary>
        [JsonPropertyName("by")]
        public string By { get; set; }

        /// <summary>
        /// Gets or sets the number of comments on the story.
        /// </summary>
        [JsonPropertyName("descendants")]
        public int Descendants { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the story.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the score of the story.
        /// </summary>
        [JsonPropertyName("score")]
        public int Score { get; set; }

        /// <summary>
        /// Gets or sets the time the story was created, in Unix time.
        /// </summary>
        [JsonPropertyName("time")]
        public int Time { get; set; }

        /// <summary>
        /// Gets or sets the title of the story.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the type of the story.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the URL of the story.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
