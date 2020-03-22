using Fun.Api.Services;
using Fun.Api.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Fun.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MediaController : ControllerBase
    {
        private readonly IMediaPlayer _mediaPlayer;
        private readonly IMediaFileNameValidator _mediaFileNameValidator;
        private readonly IDirectoryService _directoryService;
        private readonly IHubContext<VideoHub> _videoHubContext;

        public MediaController(
            IMediaPlayer mediaPlayer,
            IMediaFileNameValidator mediaFileNameValidator,
            IDirectoryService directoryService,
            IHubContext<VideoHub> videoHubContext)
        {
            _mediaPlayer = mediaPlayer;
            _mediaFileNameValidator = mediaFileNameValidator;
            _directoryService = directoryService;
            _videoHubContext = videoHubContext;
        }

        [HttpGet("play/{fileName}")]
        public IActionResult Play(string fileName)
        {
            if (_mediaFileNameValidator.Validate(fileName))
            {
                _ = _videoHubContext.Clients.All.SendAsync("PlayVideo", fileName).ConfigureAwait(false);

                return Ok();
            }

            return NotFound();
        }

        [HttpGet("stop")]
        public IActionResult Stop()
        {
            // todo send stop to clients
            _mediaPlayer.Stop();
            return Ok();
        }

        [HttpGet("list")]
        public IActionResult List()
        {
            var fileNames = _directoryService.GetMediaFileNames();
            return new JsonResult(fileNames);
        }
    }
}
