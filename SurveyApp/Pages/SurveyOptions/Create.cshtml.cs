using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SurveyApp.Models;

namespace SurveyApp.Pages.SurveyOptions
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
        ViewData["OptionId"] = new SelectList(_context.Options, "Id", "Name");
        ViewData["SurveyId"] = new SelectList(_context.Surveys, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public SurveyOption SurveyOption { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.SurveyOptions.Add(SurveyOption);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}