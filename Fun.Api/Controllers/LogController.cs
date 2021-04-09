using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Fun.Api.Controllers
{
    [Route("api/[controller]")]
    public class LogController : Controller
    {
        private readonly ILogger<LogController> _logger;

        public LogController(ILogger<LogController> logger)
        {
            _logger = logger;
        }

        [HttpPost("Error")]
        public ActionResult Error([FromBody]string message)
        {
            _logger.LogError("Javascript error occurred - " + message);
            return Ok();
        }
    }
}