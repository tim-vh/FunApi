using Microsoft.AspNetCore.SignalR;

namespace Fun.Api.Model
{
    public class Video
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string Thumbnail { get; set; }

        public void Play(IHubContext<VideoHub> videoHubContext)
        {
            videoHubContext.Clients.All.SendAsync("PlayVideo", Url).ConfigureAwait(false);
        }
    }
}
