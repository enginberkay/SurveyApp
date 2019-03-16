using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Models;

namespace SurveyApp.Pages.SurveyOptions
{
    public class DeleteModel : PageModel
    {
        private readonly SurveyApp.Models.PostgresContext _context;

        public DeleteModel(SurveyApp.Models.PostgresContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SurveyOption = await _context.SurveyOptions.FindAsync(id);

            if (SurveyOption != null)
            {
                _context.SurveyOptions.Remove(SurveyOption);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
