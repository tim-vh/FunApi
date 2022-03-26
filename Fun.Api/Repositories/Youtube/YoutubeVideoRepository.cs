using Fun.Api.Model;
using Fun.Api.Repositories.Youtube.Commands;
using Fun.Api.Repositories.Youtube.Model;
using Fun.Api.Repositories.Youtube.Queries;
using Provocq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fun.Api.Repositories.Youtube
{
    public class YoutubeVideoRepository : IVideoRepository
    {
        private readonly BlockingDataHandler<YoutubeVideoDataContext> _datacontextWrapper;

        public YoutubeVideoRepository(BlockingDataHandler<YoutubeVideoDataContext> datacontextWrapper)
        {
            _datacontextWrapper = datacontextWrapper;
        }

        public string Type => "Youtube";

        public async Task<Video> GetVideo(string url)
        {
            return await _datacontextWrapper.ExecuteQuery(new GetVideoQuery { Url = url });
        }

        public async Task<IEnumerable<Video>> GetVideos()
        {
            return await _datacontextWrapper.ExecuteQuery(new GetVideosQuery());            
        }

        public async Task AddVideo(Video video)
        {
            await _datacontextWrapper.ExecuteCommand(new AddVideoCommand
            {
                YoutubeVideo = new YoutubeVideo
                {
                    Name = video.Name,
                    Url = video.Url

                }
            });
        }
    }
}
