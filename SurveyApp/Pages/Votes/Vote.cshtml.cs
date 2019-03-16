using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SurveyApp.Models;

namespace SurveyApp.Pages.Votes
{
    public class VoteModel : PageModel
    {
        private readonly SurveyApp.Models.PostgresContext _context;

        public VoteModel(SurveyApp.Models.PostgresContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IList<SurveyOption> SurveyOption { get; set; }
        [BindProperty]
        public SurveyOption PointedOption { get; set; }

        public async Task OnGetAsync(int id)
        {
            SurveyOption = await _context.SurveyOptions
                .Include(s => s.Option)
                .Include(s => s.Survey)
                .Where(s => s.SurveyId == id).ToListAsync();
        }

        public async Task<IActionResult> OnPostVoteAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                PointedOption = await _context.SurveyOptions.FindAsync(id);
                PointedOption.Point += 1;
                _context.Attach(PointedOption).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SurveyOptionExists(PointedOption.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Surveys/Index");
        }

        private bool SurveyOptionExists(int id)
        {
            return _context.SurveyOptions.Any(e => e.Id == id);
        }
    }
}