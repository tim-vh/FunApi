using Fun.Api.Services;
using Fun.Api.Validators;
using Moq;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using Fun.Api.ApiModel;

namespace Fun.Api.Tests.Validators
{
    public class VideoUrlValidatorTests
    {
        private readonly Mock<IGetVideosQuery> _GetVideosQuery;

        private readonly VideoUrlValidator _videoUrlValidator;

        public VideoUrlValidatorTests()
        {
            _GetVideosQuery = new Mock<IGetVideosQuery>();

            _videoUrlValidator = new VideoUrlValidator(_GetVideosQuery.Object);
        }

        [Fact]
        public void Validatate_returns_true_when_video_with_url_exists()
        {
            // Arrange
            var video = new Video { Url = "Test" };
            var videos = new List<Video>
            {
                new Video { Url = "a"},
                new Video { Url = "b"},
                new Video { Url = "c"},
                video
            };

            _GetVideosQuery.Setup(d => d.Execute()).Returns(videos);

            // Act
            var isValid = _videoUrlValidator.Validate(video.Url);

            // Assert
            isValid.Should().Be(true);
        }

        [Fact]
        public void Validatate_returns_false_when_video_with_url_not_exists()
        {
            // Arrange
            var video = new Video { Url = "Test" };
            var videos = new List<Video>
            {
                new Video { Url = "a"},
                new Video { Url = "b"},
                new Video { Url = "c"},
            };

            _GetVideosQuery.Setup(d => d.Execute()).Returns(videos);

            // Act
            var isValid = _videoUrlValidator.Validate(video.Url);

            // Assert
            isValid.Should().Be(false);
        }
    }
}
