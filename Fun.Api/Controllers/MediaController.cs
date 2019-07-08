using Fun.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fun.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
    }
}
