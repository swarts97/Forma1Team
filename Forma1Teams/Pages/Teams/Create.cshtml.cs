using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Forma1Teams.Data;
using Forma1Teams.Models;
using System.ComponentModel.DataAnnotations;
using static Forma1Teams.Services.ValidatorService;

namespace Forma1Teams.Pages.Teams
{
    public class CreateModel : PageModel
    {
        private readonly Forma1Teams.Data.F1Context _context;

        public CreateModel(Forma1Teams.Data.F1Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Team newTeam = new Team()
            {
                Name = Name,
                YearOfFoundation = YearOfFoundation,
                WorldChampionNumber = WorldChampionNumber,
                EntryFeePaid = EntryFeePaid
            };

            _context.Teams.Add(newTeam);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
