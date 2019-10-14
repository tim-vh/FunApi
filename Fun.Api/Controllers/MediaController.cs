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

        public MediaController(IMediaPlayer mediaPlayer, IMediaFileNameValidator mediaFileNameValidator)
        {
            _mediaPlayer = mediaPlayer;
            _mediaFileNameValidator = mediaFileNameValidator;
        }

        [HttpGet("play/{fileName}")]
        public ActionResult Get(string fileName)
        {
            if (_mediaFileNameValidator.Validate(fileName))
            {
                _mediaPlayer.Play(fileName);

                return Ok();
            }

            return NotFound();
        }


        [HttpGet("stop")]
        public ActionResult Get()
        {
            _mediaPlayer.Stop();
            return Ok();
        }
    }
}
