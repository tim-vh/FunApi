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

        [Fact(Skip = "Generated client throws exception on 204 response")]
        public async void Play_plays_video()
        {
            // Arrange
            var client = _factory.CreateClient();
            var videoClient = new VideoClient(client);

            // Act
            var result = await videoClient.PlayAsync("https://www.youtube.com/watch?v=S7SLep244ss");

            // Assert
            result.StatusCode.Should().Be(204);
        }

        [Fact]
        public async void AddVideo_adds_youtube_video_correctly()
        {
            // Arrange
            var httpClient = _factory.CreateClient();
            
            var userClient = new UserClient(httpClient);
            var authenticationResult = await userClient.AuthenticateAsync(new LoginCredentials { Username = "testuser1", Password = "123qweasdZXC!" });
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {authenticationResult.Token}");

            var videoClient = new VideoClient(httpClient);          
            var addVideoRequest = new AddVideoRequest()
            {
                Name = "Add video test",
                Url = "https://www.youtube.com/watch?v=Video1"
            };

            // Act
            var result = await videoClient.AddVideoAsync(addVideoRequest);
            var videos = await videoClient.GetVideosAsync();

            // Assert
            result.StatusCode.Should().Be(200);
            videos.Should().Contain(v => v.Name == addVideoRequest.Name);
        }
    }
}