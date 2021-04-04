using Fun.Api.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Web;

namespace Fun.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VideoController : ControllerBase
    {
        private readonly IMediaFileNameValidator _mediaFileNameValidator;
        private readonly IHubContext<VideoHub> _videoHubContext;

        public VideoController(
            IMediaFileNameValidator mediaFileNameValidator,
            IHubContext<VideoHub> videoHubContext)
        {
            _mediaFileNameValidator = mediaFileNameValidator;
            _videoHubContext = videoHubContext;
        }

        [HttpGet("play/{url}")]
        public IActionResult Play(string url)
        {
            url = HttpUtility.UrlDecode(url);
            if (_mediaFileNameValidator.Validate(url))
            {
                _videoHubContext.Clients.All.SendAsync("PlayVideo", url).ConfigureAwait(false);

                return new NoContentResult();
            }

            return NotFound();
        }

        [HttpGet("stop")]
        public IActionResult Stop()
        {
            _videoHubContext.Clients.All.SendAsync("StopVideo").ConfigureAwait(false);
            return Ok();
        }
    }
}