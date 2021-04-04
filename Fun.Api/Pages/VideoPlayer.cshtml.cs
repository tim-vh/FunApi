using System.Collections.Generic;
using Fun.Api.Model;
using Fun.Api.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fun.Api.Pages
{
    public class FunVideoPlayerModel : PageModel
    {
        public IEnumerable<Video> Videos { get; set; }

        private readonly IGetVideosQuery _getVideosQuery;

        public FunVideoPlayerModel(IGetVideosQuery getVideosQuery)
        {
            _getVideosQuery = getVideosQuery;
        }

        public void OnGet()
        {
            Videos = _getVideosQuery.Execute();
        }
    }
}
