using Fun.Api.Model;
using Fun.Api.Queries;
using System.Collections.Generic;

namespace Fun.Api.Repositories
{
    public class WwwrootVideoRepository : IVideoRepository
    {
        private readonly IGetVideosQuery _getVideosQuery;

        public WwwrootVideoRepository(GetVideosFromWwwrootQuery getVideosQuery)
        {
            _getVideosQuery = getVideosQuery;
        }

        public IEnumerable<Video> GetVideos()
        {
            return _getVideosQuery.Execute();
        }
    }
}
