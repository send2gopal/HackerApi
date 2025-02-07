using AutoFixture;
using FluentAssertions;
using HackernNews.Api.Controllers;
using HackernNews.Core.Shared;
using HackernNews.UseCases.Stories;
using HackernNews.UseCases.Stories.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HackernNews.Api.Test
{
    public class HackerNewsControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly HackerNewsController _controller;

        public HackerNewsControllerTests()
        {
            _fixture = new Fixture();
            _mediatorMock = new Mock<IMediator>();
            _controller = new HackerNewsController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetNewStories_ShouldReturnOkResult_WithValidData()
        {
            // Arrange
            var stories = _fixture.CreateMany<StoryDto>(10).ToList();
            var pagedResult = new PagedViewModelResult<StoryDto>(stories, 1, 10, 100);
            var query = new GetNewStoriesQuery(1, 10);
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetNewStoriesQuery>(), default))
                         .ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.GetNewStories(1, 10);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(pagedResult);
            _mediatorMock.Verify(m => m.Send(It.Is<GetNewStoriesQuery>(q => q.pageNumber == 1 && q.pageSize == 10), default), Times.Once);
        }
        
        [Fact]
        public async Task GetNewStories_ShouldReturnErrorResult_WhenErrorOccurs()
        {
            // Arrange
            var error = _fixture.Build<ServiceError>()
                .With(e => e.Code, 500)
                .With(e => e.Type, "Internal Server Error")
                .With(e => e.Message, "An error occurred while processing the request.")
                .Create();
            var query = new GetNewStoriesQuery(1, 10);
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetNewStoriesQuery>(), default))
                         .ReturnsAsync(error);

            // Act
            var result = await _controller.GetNewStories(1, 10);

            // Assert
            var objectResult = result as ObjectResult;
            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be(error);
            _mediatorMock.Verify(m => m.Send(It.Is<GetNewStoriesQuery>(q => q.pageNumber == 1 && q.pageSize == 10), default), Times.Once);
        }        
    }
}
