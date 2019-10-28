using Fun.Api.Services;
using Fun.Api.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fun.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "ApiKeyPolicy")]
    public class MediaController : ControllerBase
    {
        private readonly IMediaPlayer _mediaPlayer;
        private readonly IMediaFileNameValidator _mediaFileNameValidator;
        private readonly IDirectoryService _directoryService;

        public MediaController(
            IMediaPlayer mediaPlayer,
            IMediaFileNameValidator mediaFileNameValidator,
            IDirectoryService directoryService)
        {
            _mediaPlayer = mediaPlayer;
            _mediaFileNameValidator = mediaFileNameValidator;
            _directoryService = directoryService;
        }

        [HttpGet("play/{fileName}")]
        public ActionResult Play(string fileName)
        {
            if (_mediaFileNameValidator.Validate(fileName))
            {
                _mediaPlayer.Play(fileName);

                return Ok();
            }

            return NotFound();
        }

        [HttpGet("stop")]
        public ActionResult Stop()
        {
            _mediaPlayer.Stop();
            return Ok();
        }

        [HttpGet("list")]
        public ActionResult List()
        {
            var fileNames = _directoryService.GetMediaFileNames();
            return new JsonResult(fileNames);
        }
    }
}
