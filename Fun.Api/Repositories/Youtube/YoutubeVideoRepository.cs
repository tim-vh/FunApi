using Fun.Api.Model;
using System.Collections.Generic;

namespace Fun.Api.Repositories.Youtube
{
    public class YoutubeVideoRepository : IVideoRepository
    {
        public Video GetVideo(string url)
        {
            return new Video
            {
                Name = "YT test",
                Url = "https://www.youtube.com/watch?v=xuYRglBC-Vs&list=RDxuYRglBC-Vs&start_radio=1"
            };
        }

        public IEnumerable<Video> GetVideos()
        {
            return new List<Video>
            {
                new Video
                {
                    Name = "YT test",
                    Url = "https://www.youtube.com/watch?v=xuYRglBC-Vs&list=RDxuYRglBC-Vs&start_radio=1"
                }
            };
        }
    }
}
