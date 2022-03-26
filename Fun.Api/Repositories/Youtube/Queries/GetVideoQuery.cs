using Fun.Api.Repositories.Youtube.Model;
using Provocq;
using System.Linq;

namespace Fun.Api.Repositories.Youtube.Queries
{
    public class GetVideoQuery : IQuery<YoutubeVideoDataContext, YoutubeVideo>
    {
        public string Url { get; set; }

        public YoutubeVideo Execute(YoutubeVideoDataContext context)
        {
            return context.Videos.FirstOrDefault(v => v.Url == Url);      
        }
    }
}
