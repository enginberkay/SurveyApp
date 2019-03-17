using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SurveyApp.Models;

namespace SurveyApp.Pages.Options
{
    public class CreateModel : PageModel
    {
        private readonly SurveyApp.Models.PostgresContext _context;

        public CreateModel(SurveyApp.Models.PostgresContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Option Option { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Options.Add(Option);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}