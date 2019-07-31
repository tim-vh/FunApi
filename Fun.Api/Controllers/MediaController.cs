using Fun.Api.Services;
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

        public MediaController(IMediaPlayer mediaPlayer)
        {
            _mediaPlayer = mediaPlayer;
        }

        [HttpGet("play/{fileName}")]
        public ActionResult Get(string fileName)
        {
            _mediaPlayer.Play(fileName);

            return Ok();
        }


        [HttpGet("stop")]
        public ActionResult Get()
        {
            _mediaPlayer.Stop();
            return Ok();
        }
    }
}
