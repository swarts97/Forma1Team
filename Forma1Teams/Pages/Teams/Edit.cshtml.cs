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
using Forma1Teams.Services;

namespace Forma1Teams.Pages.Teams
{
    public class EditModel : PageModel
    {
        private readonly F1Context _context;
        private readonly TeamService _teamService;

        public EditModel(F1Context context, TeamService teamService)
        {
            _context = context;
            _teamService = teamService;
        }

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

                await _teamService.TeamByIdAsync(_context, id);

                if (_teamService.Team == null)
                {
                    return NotFound();
                }

                Name = _teamService.Team.Name;
                YearOfFoundation = _teamService.Team.YearOfFoundation;
                WorldChampionNumber = _teamService.Team.WorldChampionNumber;
                EntryFeePaid = _teamService.Team.EntryFeePaid;

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

            await _teamService.TeamByIdAsync(_context, id);
            _teamService.FillTeamData(Name, YearOfFoundation, WorldChampionNumber, EntryFeePaid);

            _context.Attach(_teamService.Team).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(_teamService.Team.TeamID))
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
