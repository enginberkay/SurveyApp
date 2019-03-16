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
    public class IndexModel : PageModel
    {
        private readonly SurveyApp.Models.PostgresContext _context;

        public IndexModel(SurveyApp.Models.PostgresContext context)
        {
            _context = context;
        }

        public IList<Option> Option { get;set; }

        public async Task OnGetAsync()
        {
            Option = await _context.Options.ToListAsync();
        }
    }
}
