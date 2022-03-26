using Fun.Api.Repositories.Youtube.Model;
using Provocq;
using System.Collections.Generic;

namespace Fun.Api.Repositories.Youtube.Queries
{
    public class GetVideosQuery : IQuery<YoutubeVideoDataContext, IEnumerable<YoutubeVideo>>
    {
        public IEnumerable<YoutubeVideo> Execute(YoutubeVideoDataContext context)
        {
            return context.Videos;
        }
    }
}
