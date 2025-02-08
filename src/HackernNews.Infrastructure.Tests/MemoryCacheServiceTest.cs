using AutoFixture;
using FluentAssertions;
using HackernNews.Infrastructure.Configurations;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace HackernNews.Infrastructure.Tests
{

    /// <summary>
    /// Unit tests for the <see cref="MemoryCacheService"/> class.
    /// </summary>
    public class MemoryCacheServiceTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IMemoryCache> _cacheMock;
        private readonly Mock<IOptions<CacheSettings>> _optionsMock;
        private readonly Mock<ILogger<MemoryCacheService>> _loggerMock;
        private readonly MemoryCacheService _service;

        /// <summary>
        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryCacheServiceTests"/> class.
        /// </summary>
        public MemoryCacheServiceTests()
        {
            _fixture = new Fixture();
            _cacheMock = new Mock<IMemoryCache>();
            _optionsMock = new Mock<IOptions<CacheSettings>>();
            _loggerMock = new Mock<ILogger<MemoryCacheService>>();

            var cacheSettings = new CacheSettings { SlidingExpirationMinutes = 10 };
            _optionsMock.Setup(o => o.Value).Returns(cacheSettings);

            _service = new MemoryCacheService(_cacheMock.Object, _optionsMock.Object, _loggerMock.Object);
        }

        [Fact(DisplayName = "Get should return cached value when cache hit")]
        public void Get_ShouldReturnCachedValue_WhenCacheHit()
        {
            // Arrange
            var key = _fixture.Create<string>();
            var expectedValue = _fixture.Create<string>();
            object cacheValue = null;
            _cacheMock.Setup(c => c.TryGetValue(key, out cacheValue)).Returns(true);
            _cacheMock.Setup(c => c.CreateEntry(key)).Returns(Mock.Of<ICacheEntry>());

            // Act
            var result = _service.Get(key, () => expectedValue);

            // Assert
            result.Should().Be(expectedValue);
        }

        [Fact(DisplayName = "Get should fetch and cache value when cache miss")]
        public void Get_ShouldFetchAndCacheValue_WhenCacheMiss()
        {
            // Arrange
            var key = _fixture.Create<string>();
            var expectedValue = _fixture.Create<string>();
            object cacheValue = null;
            _cacheMock.Setup(c => c.TryGetValue(key, out cacheValue)).Returns(false);
            _cacheMock.Setup(c => c.CreateEntry(key)).Returns(Mock.Of<ICacheEntry>());

            // Act
            var result = _service.Get(key, () => expectedValue);

            // Assert
            result.Should().Be(expectedValue);
            _cacheMock.Verify(c => c.CreateEntry(key), Times.Once);
        }

        [Fact(DisplayName = "Get should cache value with absolute expiration")]
        public void Get_ShouldCacheValue_WithAbsoluteExpiration()
        {
            // Arrange
            var key = _fixture.Create<string>();
            var expectedValue = _fixture.Create<string>();
            var absoluteExpiration = TimeSpan.FromMinutes(5);
            object cacheValue = null;
            _cacheMock.Setup(c => c.TryGetValue(key, out cacheValue)).Returns(false);
            _cacheMock.Setup(c => c.CreateEntry(key)).Returns(Mock.Of<ICacheEntry>());

            // Act
            var result = _service.Get(key, () => expectedValue, absoluteExpiration);

            // Assert
            result.Should().Be(expectedValue);
            _cacheMock.Verify(c => c.CreateEntry(key), Times.Once);
        }

        [Fact(DisplayName = "Set should cache value with sliding expiration")]
        public void Set_ShouldCacheValue_WithSlidingExpiration()
        {
            // Arrange
            var key = _fixture.Create<string>();
            var value = _fixture.Create<string>();
            _cacheMock.Setup(c => c.CreateEntry(key)).Returns(Mock.Of<ICacheEntry>());

            // Act
            _service.Set(key, value);

            // Assert
            _cacheMock.Verify(c => c.CreateEntry(key), Times.Once);
        }

        [Fact(DisplayName = "TryGetValue should return true when cache hit")]
        public void TryGetValue_ShouldReturnTrue_WhenCacheHit()
        {
            // Arrange
            var key = _fixture.Create<string>();
            object cacheValue = _fixture.Create<string>();
            _cacheMock.Setup(c => c.TryGetValue(key, out cacheValue)).Returns(true);

            // Act
            var result = _service.TryGetValue(key, out object value);

            // Assert
            result.Should().BeTrue();
            value.Should().Be(cacheValue);
        }

        [Fact(DisplayName = "TryGetValue should return false when cache miss")]
        public void TryGetValue_ShouldReturnFalse_WhenCacheMiss()
        {
            // Arrange
            var key = _fixture.Create<string>();
            object cacheValue = null;
            _cacheMock.Setup(c => c.TryGetValue(key, out cacheValue)).Returns(false);

            // Act
            var result = _service.TryGetValue<string>(key, out var value);

            // Assert
            result.Should().BeFalse();
            value.Should().BeNull();
        }

        [Fact(DisplayName = "Remove should remove value from cache")]
        public void Remove_ShouldRemoveValueFromCache()
        {
            // Arrange
            var key = _fixture.Create<string>();

            // Act
            _service.Remove(key);

            // Assert
            _cacheMock.Verify(c => c.Remove(key), Times.Once);
        }
    }
}

