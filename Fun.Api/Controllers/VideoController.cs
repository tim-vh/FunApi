using Fun.Api.Model;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IHubContext<VideoHub> _videoHubContext;

        public VideoController(
            IVideoCatalog videoCatalog,
            IHubContext<VideoHub> videoHubContext)
        {
            _videoCatalog = videoCatalog ?? throw new ArgumentNullException(nameof(videoCatalog));
            _videoHubContext = videoHubContext ?? throw new ArgumentNullException(nameof(videoHubContext));
        }

        [HttpGet("play/{url}")]
        public ActionResult Play(string url)
        {
            url = HttpUtility.UrlDecode(url);
            var video = _videoCatalog.GetVideo(url);
            if (video != null)
            {
                video.Play(_videoHubContext);

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

        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult AddVideo([FromBody] AddVideoRequest addVideoRequest)
        {
            var video = new Video()
            {
                Name = addVideoRequest.Name,
                Url = addVideoRequest.Url
            };

            _videoCatalog.AddVideo(video);

            return Ok();
        }

        public class AddVideoRequest
        {
            public string Name { get; set; }

            public string Url { get; set; }
        }
    }
}