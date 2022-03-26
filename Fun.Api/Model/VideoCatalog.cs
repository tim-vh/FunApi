using System;
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
            var videos = _repositories.SelectMany(r => r.GetVideos().GetAwaiter().GetResult()).ToList();

            videos.Sort((a, b) => a.Name.CompareTo(b.Name));

            return videos;
        }

        public Video GetVideo(string url)
        {
            return _repositories.Select(r => r.GetVideo(url).GetAwaiter().GetResult()).FirstOrDefault(v => v != null);
        }

        public void AddVideo(Video video)
        {
            if (video.Url.StartsWith("https://www.youtube.com/"))
            {
                var repository = _repositories.First(r => r.Type == "Youtube");
                repository.AddVideo(video);
            }
            else
            {
                throw new NotSupportedException("This video is not supported");
            }
        }
    }
}
