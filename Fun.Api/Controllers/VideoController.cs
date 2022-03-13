using Fun.Api.Model;
using Fun.Api.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Web;

namespace Fun.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IVideoCatalog _videoCatalog;
        private readonly IVideoUrlValidator _videoUrlValidator;
        private readonly IHubContext<VideoHub> _videoHubContext;

        public VideoController(
            IVideoCatalog videoCatalog,
            IVideoUrlValidator videoUrlValidator,
            IHubContext<VideoHub> videoHubContext)
        {
            _videoCatalog = videoCatalog ?? throw new ArgumentNullException(nameof(videoCatalog));
            _videoUrlValidator = videoUrlValidator ?? throw new ArgumentNullException(nameof(videoUrlValidator));
            _videoHubContext = videoHubContext ?? throw new ArgumentNullException(nameof(videoHubContext));
        }

        [HttpGet("play/{url}")]
        public ActionResult Play(string url)
        {
            url = HttpUtility.UrlDecode(url);
            if (_videoUrlValidator.Validate(url))
            {
                _videoHubContext.Clients.All.SendAsync("PlayVideo", url).ConfigureAwait(false);

                return new NoContentResult();
            }

            return NotFound();
        }

        [HttpGet("stop")]
        public ActionResult Stop()
        {
            _videoHubContext.Clients.All.SendAsync("StopVideo").ConfigureAwait(false);
            return Ok();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Video>> GetVideos()
        {
            var videos = _videoCatalog.GetVideos();
            return Ok(videos);
        }
    }
}