using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Forma1Teams.Data;
using Forma1Teams.Models;
using static Forma1Teams.Services.ValidatorService;
using System.ComponentModel.DataAnnotations;

namespace Forma1Teams.Pages.Teams
{
    public class EditModel : PageModel
    {
        private readonly Forma1Teams.Data.F1Context _context;

        public EditModel(Forma1Teams.Data.F1Context context)
        {
            _context = context;
        }

        private int? SelectedTeamID { get; set; }

        [BindProperty]
        [Required]
        public string Name { get; set; }

        [BindProperty]
        [Required]
        [FoundationValidator]
        public int YearOfFoundation { get; set; }

        [BindProperty]
        [Required]
        [WorldChampionNumberValidator]
        public int WorldChampionNumber { get; set; }

        [BindProperty]
        public bool EntryFeePaid { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (_context._httpContextAccessor.HttpContext.User != null && _context._httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
			{
                if (id == null)
                {
                    return NotFound();
                }

                SelectedTeamID = id;
                Team team = await _context.Teams.FirstOrDefaultAsync(m => m.TeamID == SelectedTeamID);

                if (team == null)
                {
                    return NotFound();
                }

                Name = team.Name;
                YearOfFoundation = team.YearOfFoundation;
                WorldChampionNumber = team.WorldChampionNumber;
                EntryFeePaid = team.EntryFeePaid;

                return Page();
            }
            
            else
                return RedirectToPage("../Error");
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Team team = await _context.Teams.FirstOrDefaultAsync(m => m.TeamID == id);

            team.Name = Name;
            team.YearOfFoundation = YearOfFoundation;
            team.WorldChampionNumber = WorldChampionNumber;
            team.EntryFeePaid = EntryFeePaid;

            _context.Attach(team).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(team.TeamID))
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

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.TeamID == id);
        }
    }
}
