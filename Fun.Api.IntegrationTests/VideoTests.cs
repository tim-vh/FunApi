using Fun.Api.Clients;
using Fun.Api.IntegrationTests.Helpers;
using Xunit;
using FluentAssertions;

namespace Fun.Api.IntegrationTests
{
    public class VideoTests : IClassFixture<FunApiWebApplicationFactory<Program>>
    {
        private FunApiWebApplicationFactory<Program> _factory;

        public VideoTests(FunApiWebApplicationFactory<Program> factory)
        {
            _factory = factory;

        }

        [Fact]
        public async void GetVideos_returns_videos()
        {
            // Arrange
            var client = _factory.CreateClient();
            var videoClient = new VideoClient(client);

            // Act
            var videos = await videoClient.GetVideosAsync();

            // Assert
            videos.Should().Contain(v => v.Name == "TestVideo");
        }

        //[Fact]
        //public async void Play_plays_video()
        //{
        //    // Arrange
        //    var client = _factory.CreateClient();
        //    var videoClient = new VideoClient(client);

        //    // Act
        //    var result = await videoClient.PlayAsync("https://www.youtube.com/watch?v=S7SLep244ss");

        //    // Assert
        //    result.StatusCode.Should().Be(204);
        //}
    }
}