using Fun.Api.Repositories.Youtube.Model;
using System.Collections.Generic;

namespace Fun.Api.Repositories.Youtube
{
    public class YoutubeVideoDataContext
    {
        public YoutubeVideoDataContext()
        {
            Videos = new List<YoutubeVideo>();
        }

        public ICollection<YoutubeVideo> Videos { get; set; }
    }
}
