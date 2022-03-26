using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fun.Api.Model
{
    public interface IVideoRepository
    {
        public string Type { get; }

        Task<IEnumerable<Video>> GetVideos();

        Task<Video> GetVideo(string url);

        Task AddVideo(Video video);
    }
}
