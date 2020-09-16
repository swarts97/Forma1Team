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
    public class DeleteModel : PageModel
    {
        private readonly F1Context _context;

        public DeleteModel(F1Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Team Team { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (_context._httpContextAccessor.HttpContext.User != null && _context._httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
			{
                if (id == null)
                {
                    return NotFound();
                }

                Team = await _context.Teams.FirstOrDefaultAsync(m => m.TeamID == id);

                if (Team == null)
                {
                    return NotFound();
                }
                return Page();
            }

            else
                return RedirectToPage("../Error");

        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Team = await _context.Teams.FindAsync(id);

            if (Team != null)
            {
                _context.Teams.Remove(Team);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
