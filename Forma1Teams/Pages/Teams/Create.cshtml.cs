using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Forma1Teams.Data;
using Forma1Teams.Models;
using Forma1Teams.Services;
using System.ComponentModel.DataAnnotations;
using static Forma1Teams.Services.ValidatorService;

namespace Forma1Teams.Pages.Teams
{
    public class CreateModel : PageModel
    {
        private readonly F1Context _context;
        private readonly TeamService _teamService;

        public CreateModel(F1Context context, TeamService teamService)
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

        public IActionResult OnGet()
        {
            if (_context._httpContextAccessor.HttpContext.User != null && _context._httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return Page();
            else
                return RedirectToPage("../Error");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Teams.Add(_teamService.CreateNewTeam(Name, YearOfFoundation, WorldChampionNumber, EntryFeePaid));
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
