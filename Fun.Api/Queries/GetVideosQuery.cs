using System.Collections.Generic;
using System.Linq;
using Fun.Api.DataModel;
using Provocq;

namespace Fun.Api.Queries
{
    public class GetVideosQuery : IQuery<FunDataContext, IEnumerable<ApiModel.Video>>
    {
        public IEnumerable<ApiModel.Video> Execute(FunDataContext context)
        {
            return context.Videos.OrderBy(v => v.Name)
                                 .Select(v => new ApiModel.Video
                                 {
                                     Name = v.Name,
                                     Thumbnail = v.Thumbnail,
                                     Url = v.Url
                                 });
        }
    }
}
