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
            var videos = new List<Video>();

            foreach (var repository in _repositories)
            {
                videos.AddRange(repository.GetVideos());
            }

            videos.Sort((a, b) => a.Name.CompareTo(b.Name));

            return videos;
        }
    }
}
