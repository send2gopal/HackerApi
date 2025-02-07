using HackernNews.Core.Entities;
using HackernNews.Core.Interfaces;
using HackernNews.Core.Shared;
using HackernNews.Infrastructure.DownstreamServices;
using Mapster;
using Microsoft.Extensions.Logging;
using OneOf;
using Refit;

namespace HackernNews.Infrastructure.HackerNewsSource
{
    /// <summary>
    /// Service to interact with Hacker News API.
    /// </summary>
    public class HackerNewsService : IHackerNewsService
    {
        private readonly IHackerNewSourceApiClient _client;
        private readonly ILogger<HackerNewsService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HackerNewsService"/> class.
        /// </summary>
        /// <param name="client">The Hacker News API client.</param>
        /// <param name="logger">The logger instance.</param>
        public HackerNewsService(IHackerNewSourceApiClient client, ILogger<HackerNewsService> logger)
        {
            _client = client;
            _logger = logger;
        }

        /// <summary>
        /// Gets the new stories asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a OneOf type with either a list of story IDs or a service error.</returns>
        public async Task<OneOf<IEnumerable<long>, ServiceError>> GetNewStoriesAsync()
        {
            try
            {
                var stories = await _client.GetNewStoriesAsync();
                return stories.ToList();
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex, "API error occurred while making call to GetNewStoriesAsync.");
                return new ServiceError()
                {
                    Code = (int)ex.StatusCode,
                    Message = ex.Message,
                    Type = "downstream_error",
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while making call to GetNewStoriesAsync.");
                return new ServiceError()
                {
                    Code = 500,
                    Message = "Error occurred while making call to GetNewStoriesAsync.",
                    Type = "unhandled_exception",
                };
            }
        }

        /// <summary>
        /// Gets the story details asynchronously.
        /// </summary>
        /// <param name="storyId">The story identifier.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a OneOf type with either the story details or a service error.</returns>
        public async Task<OneOf<StoryDetails, ServiceError>> GetStoryDetails(long storyId)
        {
            try
            {
                var storyDetails = await _client.GetStoryDetails($"{storyId}.json");
                return TypeAdapter.Adapt<StoryDetails>(storyDetails);
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex, "API error occurred while making call to GetStoryDetails.");
                return new ServiceError()
                {
                    Code = (int)ex.StatusCode,
                    Message = ex.Message,
                    Type = "downstream_error",
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while making call to GetStoryDetails.");
                return new ServiceError()
                {
                    Code = 500,
                    Message = "Error occurred while making call to GetStoryDetails.",
                    Type = "unhandled_exception",
                };
            }
        }
    }
}
