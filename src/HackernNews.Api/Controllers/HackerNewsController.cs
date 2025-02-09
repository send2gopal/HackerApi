using HackernNews.UseCases.Stories.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HackernNews.Api.Controllers
{

    /// <summary>
    /// Controller for handling Hacker News related requests.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class HackerNewsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="HackerNewsController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator instance.</param>
        public HackerNewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets the new stories from Hacker News.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of stories per page.</param>
        /// <returns>A list of new stories.</returns>
        [Route("new-stories")]
        [HttpGet]
        public async Task<IActionResult> GetNewStories([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(new GetNewStoriesQuery(pageNumber, pageSize));
            return result.Match(Ok, error => new ObjectResult(error) { StatusCode = error.Code });
        }
    }
}
