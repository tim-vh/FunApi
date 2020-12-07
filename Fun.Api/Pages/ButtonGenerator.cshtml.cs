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

        [BindProperty]
        public int NrOfColumns { get; set; }

        public string GridTemplateColumnsCss
        {
            get
            {
                var percentage = 100 / NrOfColumns;
                var gridTemplateColumnsCss = string.Empty;
                for (int i = 0; i < NrOfColumns; i++)
                {
                    gridTemplateColumnsCss += percentage.ToString() + "% ";
                }
                return gridTemplateColumnsCss;
            }
        }

        public string BaseUrl { get; set; }

        public ActionResult OnGet()
        {
            return Redirect(Url.Page("/ButtonSettings"));
        }

        public void OnPost()
        {
            BaseUrl = $"{Request.Scheme}://{Request.Host}";
            NrOfColumns = NrOfColumns < 1 ? NrOfColumns = 1 : NrOfColumns;
        }
    }
}