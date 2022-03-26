using Fun.Api.Model;
using Fun.Api.Repositories.Wwwroot.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fun.Api.Repositories.Wwwroot
{
    public class WwwrootVideoRepository : IVideoRepository
    {
        public string Type => "Wwwroot";

        private readonly GetVideosFromWwwrootQuery _getVideosQuery;

        public WwwrootVideoRepository(GetVideosFromWwwrootQuery getVideosQuery)
        {
            _getVideosQuery = getVideosQuery;
        }

        public async Task<Video> GetVideo(string url)
        {
            var videos = await GetVideos();
            return videos.FirstOrDefault(v => v.Url == url);
        }

        public Task<IEnumerable<Video>> GetVideos()
        {
            return Task.FromResult(_getVideosQuery.Execute());
        }

        public Task AddVideo(Video video)
        {
            throw new System.NotImplementedException();
        }
    }
}
