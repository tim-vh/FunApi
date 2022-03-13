using System.Collections.Generic;
using System.Linq;

namespace Fun.Api.Model
{
    public class VideoCatalog : IVideoCatalog
    {
        private readonly IEnumerable<IVideoRepository> _repositories;

        public VideoCatalog(IEnumerable<IVideoRepository> repositories)
        {
            _repositories = repositories;
        }

        public IEnumerable<Video> GetVideos()
        {
            var videos = _repositories.SelectMany(r => r.GetVideos()).ToList();

            videos.Sort((a, b) => a.Name.CompareTo(b.Name));

            return videos;
        }

        public Video GetVideo(string url)
        {
            return _repositories.Select(r => r.GetVideo(url)).FirstOrDefault();
        }
    }
}
