using Fun.Api.Model;
using System;
using System.Linq;

namespace Fun.Api.Validators
{
    public class VideoUrlValidator : IVideoUrlValidator
    {
        private readonly IVideoCatalog _videoCatalog;

        public VideoUrlValidator(IVideoCatalog videoCatalog)
        {
            _videoCatalog = videoCatalog ?? throw new ArgumentNullException(nameof(videoCatalog));
        }

        public bool Validate(string url)
        {
            var videos = _videoCatalog.GetVideos();
            return videos.Any(v => v.Url == url);
        }
    }
}
