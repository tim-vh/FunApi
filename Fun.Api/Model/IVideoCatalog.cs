using System.Collections.Generic;

namespace Fun.Api.Model
{
    public interface IVideoCatalog
    {
        IEnumerable<Video> GetVideos();

        Video GetVideo(string url);
        void AddVideo(Video video);
    }
}