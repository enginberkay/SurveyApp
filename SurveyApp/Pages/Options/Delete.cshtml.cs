using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Models;

namespace SurveyApp.Pages.Options
{
    public class DeleteModel : PageModel
    {
        private readonly SurveyApp.Models.PostgresContext _context;

        public DeleteModel(SurveyApp.Models.PostgresContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Option Option { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Option = await _context.Options.FirstOrDefaultAsync(m => m.Id == id);

            if (Option == null)
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

            Option = await _context.Options.FindAsync(id);

            if (Option != null)
            {
                _context.Options.Remove(Option);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
