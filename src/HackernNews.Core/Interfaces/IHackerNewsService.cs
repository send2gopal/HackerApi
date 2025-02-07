using HackernNews.Core.Entities;
using HackernNews.Core.Shared;
using OneOf;

namespace HackernNews.Core.Interfaces
{
    /// <summary>
    /// Interface for Hacker News service.
    /// </summary>
    public interface IHackerNewsService
    {
        /// <summary>
        /// Gets the new stories asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a OneOf type with either a list of story IDs or a service error.</returns>
        Task<OneOf<IEnumerable<long>, ServiceError>> GetNewStoriesAsync();

        /// <summary>
        /// Gets the story details asynchronously.
        /// </summary>
        /// <param name="storyId">The story identifier.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a OneOf type with either the story details or a service error.</returns>
        Task<OneOf<StoryDetails, ServiceError>> GetStoryDetails(long storyId);
    }
}
