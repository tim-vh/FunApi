using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fun.Api.Pages
{
    [IgnoreAntiforgeryToken]
    public class ButtonGeneratorModel : PageModel
    {
        [BindProperty]
        public IEnumerable<string> MediaFileNames { get; set; }

        [BindProperty]
        public string ApiKey { get; set; }

        public string BaseUrl { get; set; }

        public ActionResult OnGet()
        {
            return Redirect(Url.Page("/ButtonSettings"));
        }

        public void OnPost()
        {
            BaseUrl = $"{Request.Scheme}://{Request.Host}";
        }
    }
}