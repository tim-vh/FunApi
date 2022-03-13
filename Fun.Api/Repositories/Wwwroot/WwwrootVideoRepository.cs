using Fun.Api.Model;
using Fun.Api.Repositories.Wwwroot.Queries;
using System.Collections.Generic;

namespace Fun.Api.Repositories.Wwwroot
{
    public class WwwrootVideoRepository : IVideoRepository
    {
        private readonly GetVideosFromWwwrootQuery _getVideosQuery;

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
