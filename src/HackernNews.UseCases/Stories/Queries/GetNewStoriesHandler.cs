using HackernNews.Core.Interfaces;
using HackernNews.Core.Shared;
using MediatR;
using OneOf;

namespace HackernNews.UseCases.Stories.Queries
{
    /// <summary>
    /// Query to get new stories with pagination.
    /// </summary>
    public record GetNewStoriesQuery(int pageNumber, int pageSize) : IRequest<OneOf<PagedViewModelResult<StoryDto>, ServiceError>>;

    /// <summary>
    /// Handler for GetNewStoriesQuery.
    /// </summary>
    public class GetNewStoriesHandler(IHackerNewsService _newsService, ICacheService _cacheService)
        : IRequestHandler<GetNewStoriesQuery, OneOf<PagedViewModelResult<StoryDto>, ServiceError>>
    {
        /// <summary>
        /// Handles the GetNewStoriesQuery.
        /// </summary>
        /// <param name="request">The query request containing pagination parameters.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains either a paged view model result of StoryDto or a service error.</returns>
        public async Task<OneOf<PagedViewModelResult<StoryDto>, ServiceError>> Handle(GetNewStoriesQuery request, CancellationToken cancellationToken)
        {
            var result = await _newsService.GetNewStoriesAsync();
            return await result.Match<Task<OneOf<PagedViewModelResult<StoryDto>, ServiceError>>>(
                async stories =>
                {
                    // MIMIC PAGING
                    // Skip the stories based on the page number and take the number of stories based on the page size
                    var pagedStories = stories.Skip(request.pageSize * (request.pageNumber - 1)).Take(request.pageSize);

                    // Fetch the details of each story in the paged stories
                    var storyDetailsTasks = pagedStories.Select(id =>
                    {
                        return _cacheService.Get(id.ToString(), async () =>
                        {
                            // Get the story details from the news service
                            var detailsResult = await _newsService.GetStoryDetails(id);

                            // Return the story details if successful, otherwise return null
                            return detailsResult.Match(
                                Success => Success,
                                error => null
                            );
                        });
                    });

                    var storyDetails = await Task.WhenAll(storyDetailsTasks);

                    // Convert the story details to StoryDto objects
                    var storyDtos = storyDetails.Where(s=> s!=null).Select(sd => new StoryDto(sd.Id, sd.By, sd.Descendants, sd.Score, sd.Time, sd.Title, sd.Type, sd.Url)).ToList();
                    // Return the paged result with the story DTOs
                    return new PagedViewModelResult<StoryDto>(storyDtos, request.pageNumber, request.pageSize, stories.Count());
                },
                async error => await Task.FromResult(error)
            );
        }
    }
}
