﻿using Fun.Api.DataModel;
using Fun.Api.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Provocq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace Fun.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly BlockingDataHandler<FunDataContext> _dataHandler;
        private readonly IHubContext<VideoHub> _videoHubContext;

        public VideoController(
            IHubContext<VideoHub> videoHubContext,
            BlockingDataHandler<FunDataContext> dataHandler) // TODO: use interface
        {
            _videoHubContext = videoHubContext ?? throw new System.ArgumentNullException(nameof(videoHubContext));
            _dataHandler = dataHandler ?? throw new System.ArgumentNullException(nameof(dataHandler));
        }

        [HttpGet("play/{url}")]
        public async Task<ActionResult> Play(string url)
        {
            url = HttpUtility.UrlDecode(url);
            var videoExistsQuery = new VideoExistsQuery(url);
            if (await _dataHandler.ExecuteQuery(videoExistsQuery))
            {
                _ = _videoHubContext.Clients.All.SendAsync("PlayVideo", url).ConfigureAwait(false);

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
        public async Task<ActionResult<IEnumerable<ApiModel.Video>>> GetVideos()
        {
            var query = new GetVideosQuery();
            var videos = await _dataHandler.ExecuteQuery(query);
            return Ok(videos);
        }
    }
}