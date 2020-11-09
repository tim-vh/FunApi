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
        private readonly IMediaFileNameValidator _mediaFileNameValidator;
        private readonly IDirectoryService _directoryService;
        private readonly IHubContext<VideoHub> _videoHubContext;

        public MediaController(
            IMediaFileNameValidator mediaFileNameValidator,
            IDirectoryService directoryService,
            IHubContext<VideoHub> videoHubContext)
        {
            _mediaFileNameValidator = mediaFileNameValidator;
            _directoryService = directoryService;
            _videoHubContext = videoHubContext;
        }

        [HttpGet("play/{fileName}")]
        public IActionResult Play(string fileName)
        {
            if (_mediaFileNameValidator.Validate(fileName))
            {
                _videoHubContext.Clients.All.SendAsync("PlayVideo", fileName).ConfigureAwait(false);

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

        [HttpGet("list")]
        public IActionResult List()
        {
            var fileNames = _directoryService.GetMediaFileNames();
            return new JsonResult(fileNames);
        }
    }
}
