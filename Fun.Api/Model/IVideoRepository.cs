using System.Collections.Generic;

namespace Fun.Api.Model
{
    public interface IVideoRepository
    {
        IEnumerable<Video> GetVideos();

        Video GetVideo(string url);
    }
}
