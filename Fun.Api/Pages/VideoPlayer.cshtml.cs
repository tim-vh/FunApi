using System.Collections.Generic;
using Fun.Api.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fun.Api.Pages
{
    public class FunVideoPlayerModel : PageModel
    {
        public IEnumerable<string> MediaFileNames { get; set; }

        private readonly IDirectoryService _directoryService;

        public FunVideoPlayerModel(IDirectoryService directoryService)
        {
            _directoryService = directoryService;
        }

        public void OnGet()
        {
            MediaFileNames = _directoryService.GetMediaFileNames();
        }
    }
}
