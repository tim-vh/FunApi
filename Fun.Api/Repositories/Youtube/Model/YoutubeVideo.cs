using Microsoft.AspNetCore.SignalR;
using VideoLibrary;
using Video = Fun.Api.Model.Video;

namespace Fun.Api.Repositories.Youtube.Model
{
    public class YoutubeVideo : Video
    {
        public string YoutubeUrl { get; set; }

        public override void Play(IHubContext<VideoHub> videoHubContext)
        {
            var youTube = YouTube.Default;
            var youtubeVideo = youTube.GetVideo(Url);
            YoutubeUrl = youtubeVideo.Uri;

            SendPlayMessageToClients(videoHubContext, YoutubeUrl);
        }
    }
}
