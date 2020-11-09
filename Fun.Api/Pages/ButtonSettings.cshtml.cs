using System.Collections.Generic;
using Fun.Api.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fun.Api.Pages
{
    public class ButtonSettingsModel : PageModel
    {
        public IEnumerable<string> MediaFileNames { get; set; }

        private readonly IDirectoryService _directoryService;

        public ButtonSettingsModel(IDirectoryService directoryService)
        {
            _directoryService = directoryService;
        }

        public void OnGet()
        {
            MediaFileNames = _directoryService.GetMediaFileNames();
        }
    }
}