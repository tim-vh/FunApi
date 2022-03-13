using Microsoft.AspNetCore.SignalR;

namespace Fun.Api.Model
{
    public class Video
    {
        private const string PlayCommand = "PlayVideo";

        public string Name { get; set; }

        public string Url { get; set; }

        public string Thumbnail { get; set; }

        public virtual void Play(IHubContext<VideoHub> videoHubContext)
        {
            SendPlayMessageToClients(videoHubContext, Url);
        }

        protected void SendPlayMessageToClients(IHubContext<VideoHub> videoHubContext, string url)
        {
            videoHubContext.Clients.All.SendAsync(PlayCommand, url).ConfigureAwait(false);
        }
    }
}
