using AutoFixture;
using FluentAssertions;
using HackernNews.Infrastructure.DownstreamServices;
using HackernNews.Infrastructure.HackerNewsSource;
using Microsoft.Extensions.Logging;
using Moq;
using Refit;
using System.Net;

namespace HackernNews.Infrastructure.Tests
{

    /// <summary>
    /// Unit tests for the <see cref="HackerNewsService"/> class.
    /// </summary>
    public class HackerNewsServiceTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<IHackerNewSourceApiClient> _clientMock;
        private readonly Mock<ILogger<HackerNewsService>> _loggerMock;
        private readonly HackerNewsService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="HackerNewsServiceTest"/> class.
        /// </summary>
        public HackerNewsServiceTest()
        {
            _fixture = new Fixture();
            _clientMock = new Mock<IHackerNewSourceApiClient>();
            _loggerMock = new Mock<ILogger<HackerNewsService>>();
            _service = new HackerNewsService(_clientMock.Object, _loggerMock.Object);
        }

        [Fact(DisplayName = "Should return story IDs when API call is successful")]
        public async Task GetNewStoriesAsync_ShouldReturnStoryIds_WhenApiCallIsSuccessful()
        {
            // Arrange
            var storyIds = _fixture.CreateMany<long>(10);
            _clientMock.Setup(c => c.GetNewStoriesAsync())
                       .ReturnsAsync(storyIds);

            // Act
            var result = await _service.GetNewStoriesAsync();

            // Assert
            result.IsT0.Should().BeTrue();
            result.AsT0.Should().BeEquivalentTo(storyIds);
        }

        [Fact(DisplayName = "Should return service error when API exception occurs")]
        public async Task GetNewStoriesAsync_ShouldReturnServiceError_WhenApiExceptionOccurs()
        {
            // Arrange
            var apiException = await ApiException.Create(new HttpRequestMessage(), HttpMethod.Get, new HttpResponseMessage(HttpStatusCode.BadRequest), new RefitSettings());
            _clientMock.Setup(c => c.GetNewStoriesAsync())
                       .ThrowsAsync(apiException);

            // Act
            var result = await _service.GetNewStoriesAsync();

            // Assert
            result.IsT1.Should().BeTrue();
            result.AsT1.Code.Should().Be((int)HttpStatusCode.BadRequest);
            result.AsT1.Type.Should().Be("downstream_error");
        }

        [Fact(DisplayName = "Should return service error when unhandled exception occurs")]
        public async Task GetNewStoriesAsync_ShouldReturnServiceError_WhenUnhandledExceptionOccurs()
        {
            // Arrange
            var exception = new Exception("Unhandled exception");
            _clientMock.Setup(c => c.GetNewStoriesAsync())
                       .ThrowsAsync(exception);

            // Act
            var result = await _service.GetNewStoriesAsync();

            // Assert
            result.IsT1.Should().BeTrue();
            result.AsT1.Code.Should().Be(500);
            result.AsT1.Type.Should().Be("unhandled_exception");
        }

        [Fact(DisplayName = "Should return story details when API call is successful")]
        public async Task GetStoryDetails_ShouldReturnStoryDetails_WhenApiCallIsSuccessful()
        {
            // Arrange
            var storyDetailsResponse = _fixture.Create<StoryDetailsResponse>();
            _clientMock.Setup(c => c.GetStoryDetails(It.IsAny<string>()))
                       .ReturnsAsync(storyDetailsResponse);

            // Act
            var result = await _service.GetStoryDetails(storyDetailsResponse.Id);

            // Assert
            result.IsT0.Should().BeTrue();
            result.AsT0.Should().BeEquivalentTo(storyDetailsResponse);
        }

        [Fact(DisplayName = "Should return service error when API exception occurs")]
        public async Task GetStoryDetails_ShouldReturnServiceError_WhenApiExceptionOccurs()
        {
            // Arrange
            var apiException = await ApiException.Create(new HttpRequestMessage(), HttpMethod.Get, new HttpResponseMessage(HttpStatusCode.BadRequest), new RefitSettings());
            _clientMock.Setup(c => c.GetStoryDetails(It.IsAny<string>()))
                       .ThrowsAsync(apiException);

            // Act
            var result = await _service.GetStoryDetails(1);

            // Assert
            result.IsT1.Should().BeTrue();
            result.AsT1.Code.Should().Be((int)HttpStatusCode.BadRequest);
            result.AsT1.Type.Should().Be("downstream_error");
        }

        [Fact(DisplayName = "Should return service error when unhandled exception occurs")]
        public async Task GetStoryDetails_ShouldReturnServiceError_WhenUnhandledExceptionOccurs()
        {
            // Arrange
            var exception = new Exception("Unhandled exception");
            _clientMock.Setup(c => c.GetStoryDetails(It.IsAny<string>()))
                       .ThrowsAsync(exception);

            // Act
            var result = await _service.GetStoryDetails(1);

            // Assert
            result.IsT1.Should().BeTrue();
            result.AsT1.Code.Should().Be(500);
            result.AsT1.Type.Should().Be("unhandled_exception");
        }
    }
}
