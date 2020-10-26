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

        public void OnGet()
        {
            Response.Redirect(Url.Page("/ButtonSettings"));
        }
    }
}