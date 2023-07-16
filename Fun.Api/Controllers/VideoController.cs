using Fun.Api.Model;
using Fun.Api.Services;
using Fun.Api.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Fun.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IGetVideosQuery _getVideosQuery;
        private readonly IHubContext<VideoHub> _videoHubContext;
        private readonly ILogger<VideoController> _logger;

        public VideoController(
            IGetVideosQuery getVideosQuery,
            IHubContext<VideoHub> videoHubContext,
            ILogger<VideoController> logger)
        {
            _getVideosQuery = getVideosQuery ?? throw new System.ArgumentNullException(nameof(getVideosQuery));
            _videoHubContext = videoHubContext ?? throw new System.ArgumentNullException(nameof(videoHubContext));
            _logger = logger;
        }

        [HttpGet("play/{filename}")]
        public ActionResult Play(string filename)
        {
            var video = _getVideosQuery.Execute().FirstOrDefault(v => v.Filename == filename);

            if (video != null)
            {
                _videoHubContext.Clients.All.SendAsync("PlayVideo", video.Url).ConfigureAwait(false);

                return new NoContentResult();
            }

            _logger.LogInformation($"Play video reiceived an invalid url: {filename}");

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
            var videos = _getVideosQuery.Execute();
            return Ok(videos);
        }
    }
}