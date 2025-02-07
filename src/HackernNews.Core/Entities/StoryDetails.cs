namespace HackernNews.Core.Entities
{
    /// <summary>
    /// Represents the details of a story.
    /// </summary>
    public class StoryDetails
    {
        /// <summary>
        /// Gets or sets the unique identifier of the story.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the author of the story.
        /// </summary>
        public string By { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the number of descendants of the story.
        /// </summary>
        public int Descendants { get; set; }

        /// <summary>
        /// Gets or sets the score of the story.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Gets or sets the time the story was created.
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// Gets or sets the title of the story.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type of the story.
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the URL of the story.
        /// </summary>
        public string Url { get; set; } = string.Empty;
    }
}
