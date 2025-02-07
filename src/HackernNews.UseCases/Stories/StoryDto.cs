namespace HackernNews.UseCases.Stories
{
    /// <summary>  
    /// Represents a story data transfer object.  
    /// </summary>  
    public record StoryDto(
        /// <summary>  
        /// Gets the unique identifier of the story.  
        /// </summary>  
        int Id,

        /// <summary>  
        /// Gets the author of the story.  
        /// </summary>  
        string By,

        /// <summary>  
        /// Gets the number of descendants of the story.  
        /// </summary>  
        int Descendants,

        /// <summary>  
        /// Gets the score of the story.  
        /// </summary>  
        int Score,

        /// <summary>  
        /// Gets the time the story was created.  
        /// </summary>  
        int Time,

        /// <summary>  
        /// Gets the title of the story.  
        /// </summary>  
        string Title,

        /// <summary>  
        /// Gets the type of the story.  
        /// </summary>  
        string Type,

        /// <summary>  
        /// Gets the URL of the story.  
        /// </summary>  
        string Url
    );
}
