using Fun.Api.Model;
using Fun.Api.Services;
using Fun.Api.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Web;

namespace Fun.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IGetVideosQuery _getVideosQuery;
        private readonly IVideoUrlValidator _videoUrlValidator;
        private readonly IHubContext<VideoHub> _videoHubContext;

        public VideoController(
            IGetVideosQuery getVideosQuery,
            IVideoUrlValidator videoUrlValidator,
            IHubContext<VideoHub> videoHubContext)
        {
            _getVideosQuery = getVideosQuery ?? throw new System.ArgumentNullException(nameof(getVideosQuery));
            _videoUrlValidator = videoUrlValidator ?? throw new System.ArgumentNullException(nameof(videoUrlValidator));
            _videoHubContext = videoHubContext ?? throw new System.ArgumentNullException(nameof(videoHubContext));
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
            var videos = _getVideosQuery.Execute();
            return Ok(videos);
        }
    }
}