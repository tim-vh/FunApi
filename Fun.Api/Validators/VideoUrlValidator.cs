using Fun.Api.Services;
using System.Linq;

namespace Fun.Api.Validators
{
    public class VideoUrlValidator : IVideoUrlValidator
    {
        private readonly IGetVideosQuery _videosQuery;

        public VideoUrlValidator(IGetVideosQuery videosQuery)
        {
            _videosQuery = videosQuery;
        }

        public bool Validate(string url)
        {
            var videos = _videosQuery.Execute();
            return videos.Any(v => v.Url == url);
        }
    }
}
