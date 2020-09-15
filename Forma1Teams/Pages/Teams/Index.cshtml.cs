using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Forma1Teams.Data;
using Forma1Teams.Models;

namespace Forma1Teams.Pages.Teams
{
    public class IndexModel : PageModel
    {
        private readonly Forma1Teams.Data.F1Context _context;

        public IndexModel(Forma1Teams.Data.F1Context context)
        {
            _context = context;
        }

        public IList<Team> Teams { get;set; }

        public async Task OnGetAsync()
        {
            Teams = await _context.Teams.ToListAsync();
        }
    }
}
