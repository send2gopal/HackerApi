using HackernNews.Infrastructure.HackerNewsSource;
using Refit;

namespace HackernNews.Infrastructure.DownstreamServices
{
    /// <summary>
    /// Interface for accessing Hacker News API endpoints.
    /// </summary>
    public interface IHackerNewSourceApiClient
    {
        /// <summary>
        /// Retrieves a list of new story IDs.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable of new story IDs.</returns>
        [Get("/v0/newstories.json")]
        public Task<IEnumerable<long>> GetNewStoriesAsync();

        /// <summary>
        /// Retrieves the details of a specific story.
        /// </summary>
        /// <param name="storyItem">The ID of the story item.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the story details.</returns>
        [Get("/v0/item/{storyItem}")]
        public Task<StoryDetailsResponse> GetStoryDetails(string storyItem);
    }
}
