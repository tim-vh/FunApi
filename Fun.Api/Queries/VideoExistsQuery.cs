using Fun.Api.DataModel;
using Provocq;
using System.Linq;

namespace Fun.Api.Queries
{
    public class VideoExistsQuery : IQuery<FunDataContext, bool>
    {
        public VideoExistsQuery(string url)
        {
            Url = url;
        }

        public string Url { get; }

        public bool Execute(FunDataContext context)
        {
            return context.Videos.Any(v => v.Url == Url);
        }
    }
}
