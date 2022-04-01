using Fun.Api.Model;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Fun.Api.UnitTests.Model
{
    public class VideoCatalogTests
    {
        [Fact]
        public void AddVideo_stores_youtube_videos_in_the_youtube_repository()
        {
            // Arrange
            var youtubeRepositoryMock = new Mock<IVideoRepository>();
            youtubeRepositoryMock.SetupGet(y => y.Type).Returns("Youtube");
            var repositories = new List<IVideoRepository>();
            repositories.Add(youtubeRepositoryMock.Object);

            var videoCatalog = new VideoCatalog(repositories);
            var video = new Video
            {
                Name = "Test",
                Url = "https://www.youtube.com/testvideo1"
            };

            // Act
            videoCatalog.AddVideo(video);

            // Assert
            youtubeRepositoryMock.Verify(y => y.AddVideo(video));  
        }
    }
}
