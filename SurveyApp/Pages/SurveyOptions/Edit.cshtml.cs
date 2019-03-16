using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Models;

namespace SurveyApp.Pages.SurveyOptions
{
    public class EditModel : PageModel
    {
        private readonly SurveyApp.Models.PostgresContext _context;

        public EditModel(SurveyApp.Models.PostgresContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SurveyOption SurveyOption { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SurveyOption = await _context.SurveyOptions
                .Include(s => s.Option)
                .Include(s => s.Survey).FirstOrDefaultAsync(m => m.Id == id);

            if (SurveyOption == null)
            {
                return NotFound();
            }
           ViewData["OptionId"] = new SelectList(_context.Options, "Id", "Name");
           ViewData["SurveyId"] = new SelectList(_context.Surveys, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(SurveyOption).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SurveyOptionExists(SurveyOption.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SurveyOptionExists(int id)
        {
            return _context.SurveyOptions.Any(e => e.Id == id);
        }
    }
}
